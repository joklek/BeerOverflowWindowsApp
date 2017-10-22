using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Device.Location;
using System.Configuration;
using BeerOverflowWindowsApp.BarProviders;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace BeerOverflowWindowsApp
{
    public partial class MainWindow : Form
    {
        private BarRating _barRating = null;
        private BarData _selectedBar = null;
        private readonly string _defaultLatitude = ConfigurationManager.AppSettings["defaultLatitude"];
        private readonly string _defaultLongitude = ConfigurationManager.AppSettings["defaultLongitude"];
        private readonly string _defaultRadius = ConfigurationManager.AppSettings["defaultRadius"];

        public MainWindow()
        {
            InitializeComponent();
            _barRating = new BarRating();

            LatitudeTextBox.Text = _defaultLatitude;
            LongitudeTextBox.Text = _defaultLongitude;
            RadiusTextBox.Text = _defaultRadius;
        }

        public void ReLoadDataGrid(bool completely = false)
        {
            if (completely)
            {
                BarDataGridView_ClearHeaderSortGlyphs();
            }

            var barData = _barRating.BarsData;
            BarDataGridView.Rows.Clear();
            var currentLatitude = Convert.ToDouble(GetLatitude(), CultureInfo.InvariantCulture);
            var currentLongitude = Convert.ToDouble(GetLongitude(), CultureInfo.InvariantCulture);
            var currentLocation = new GeoCoordinate(currentLatitude, currentLongitude);
            foreach (var bar in barData)
            {
                var rating = bar.Ratings?.Average().ToString("0.00") ?? "0";
                var barLocation = new GeoCoordinate(bar.Latitude, bar.Longitude);
                var distance = currentLocation.GetDistanceTo(barLocation).ToString("0");
                BarDataGridView.Rows.Add(bar.Title, rating, distance);               
            }
            if (BarDataGridView.Rows.Count > 0 && _selectedBar != null)
            {
                var indexOfBar = _barRating.BarsData.FindIndex(x => x.Title == _selectedBar.Title);
                BarDataGridView.Rows[indexOfBar].Selected = true;
            }
            ClearSelection();
        }

        private void ClearSelection()
        {
            BarDataGridView.ClearSelection();
            _selectedBar = null;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            var latitude = GetLatitude();
            var longitude = GetLongitude();
            var radius = GetRadius();

            CurrentLocation.currentLocation = new GeoCoordinate(double.Parse(latitude, CultureInfo.InvariantCulture),
                                                                double.Parse(longitude, CultureInfo.InvariantCulture));

            if (!LatitudeTextIsCorrect() || !LongitudeTextIsCorrect() || !RadiusTextIsCorrect())
            {
                MessageBox.Show("Please enter correct required data. Erroneus data is painted red.");
            }
            else
            {
                var providerList = new List<object>
                {
                    new GetBarListGoogle(),
                    new GetBarListFourSquare(),
                    new GetBarListFacebook(),
                    new GetBarListTripAdvisor()
                };
                var providerCount = providerList.Count;
                var progressStep = 100 / providerCount;
                var result = new BarDataModel();
                
                var currentProgressValue = 0;
                GoButton.Enabled = false;
                InitiateProgressBars();
                UpdateProgressBars(currentProgressValue);

                foreach (IBeerable provider in providerList)
                {
                    CollectBarsFromProvider(provider, result, latitude, longitude, radius);
                    currentProgressValue += progressStep;
                    UpdateProgressBars(currentProgressValue);
                }

                HideProgressBars();
                
                // Display
                result.GetRatings();
                _barRating.BarsData = result;
                SortList(CompareType.Distance, SortOrder.Ascending);
                Application.DoEvents();        // no idea what this does. Some threading stuff, but makes button disabling work
                GoButton.Enabled = true;
            }
        }

        private void InitiateProgressBars()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
            progressBar.Value = 0;
        }

        private void HideProgressBars()
        {
            TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress);
            progressBar.Value = 0;
        }

        private void UpdateProgressBars(int currentValue)
        {
            TaskbarManager.Instance.SetProgressValue(currentValue, 100);
            // this is a big workaround for progressbar not updating as fast as we need it. 
            // TODO: Fix this when we do threads
            progressBar.Maximum = 101;
            progressBar.Value = currentValue + 1;
            progressBar.Value = currentValue;
            progressBar.Maximum = 100;
        }

        private void CollectBarsFromProvider(IBeerable provider, BarDataModel barList,
            string latitude, string longitude, string radius)
        {
            try
            {
                barList.AddRange(provider.GetBarsAround(latitude, longitude, radius));
                barList.CleanUpList(latitude, longitude, radius);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong with the message: " + exception.Message);
            }
        }

        private string GetLatitude()
        {
            return LatitudeTextBox.Text;
        }

        private string GetLongitude()
        {
            return LongitudeTextBox.Text;
        }

        private string GetRadius()
        {
            return RadiusTextBox.Text;
        }

        private void ManualBarRating_Click(object sender, EventArgs e)
        {
            var rating = manualBarRating.Rating;
            if (_selectedBar != null && rating != "" && int.TryParse(rating, out var ratingNumber))
            {
                _barRating.AddRating(_selectedBar ,ratingNumber);
                ReSort();
            }
        }

        private void BarDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (BarDataGridView.CurrentRow != null)
            {
                var selectedBarName = (string) BarDataGridView.CurrentRow.Cells["titleColumn"].Value;
                _selectedBar = _barRating.BarsData.Find(bar => bar.Title == selectedBarName);
            }
        }

        private void LongitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!LongitudeTextIsCorrect())
            {
                PaintTextBoxIncorrect(LongitudeTextBox);
            }
            else { ResetTextBoxColor(LongitudeTextBox); }
        }

        private bool LongitudeTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(LongitudeTextBox.Text,
                @"^(-?(?:1[0-7]|[1-9])?\d(?:\.\d{1,})?|180(?:\.0{1,})?)$");
        }

        private void LatitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!LatitudeTextIsCorrect())
            {
                PaintTextBoxIncorrect(LatitudeTextBox);
            }
            else { ResetTextBoxColor(LatitudeTextBox); }
        }

        private bool LatitudeTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(LatitudeTextBox.Text,
                @"^(-?[1-8]?\d(?:\.\d{1,})?|90(?:\.0{1,})?)$");
        }

        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!RadiusTextIsCorrect())
            {
                PaintTextBoxIncorrect(RadiusTextBox);
            }
            else { ResetTextBoxColor(RadiusTextBox); }
        }

        private void PaintTextBoxIncorrect(TextBox textBox)
        {
            textBox.ForeColor = Color.White;
            textBox.BackColor = Color.Red;
        }

        private void ResetTextBoxColor(TextBox textBox)
        {
            textBox.ResetForeColor();
            textBox.ResetBackColor();
        }

        private bool RadiusTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(RadiusTextBox.Text,
                @"^[0-9]{1,3}$");
        }

        private void SortList(CompareType compareType, SortOrder sortOrder = SortOrder.Ascending)
        {
            var columnIndex = (int)compareType - 1;
            var isAscending = sortOrder == SortOrder.Ascending;

            if (sortOrder != SortOrder.None)
            {
                _barRating.Sort(compareType, isAscending);
                BarDataGridView_ClearHeaderSortGlyphs();
                BarDataGridView.Columns[columnIndex].HeaderCell.SortGlyphDirection = sortOrder;
                ReLoadDataGrid();
            }
        }

        private void ReSort()
        {
            var currentSortOrder = SortOrder.None;
            var currentSortColumn = CompareType.None;

            foreach (DataGridViewColumn column in BarDataGridView.Columns)
            {
                if (column.HeaderCell.SortGlyphDirection != SortOrder.None)
                {
                    currentSortOrder = column.HeaderCell.SortGlyphDirection;
                    currentSortColumn = (CompareType) column.Index + 1;
                    break;
                }
            }

            if (currentSortOrder != SortOrder.None && currentSortColumn != CompareType.None)
            {
                SortList(currentSortColumn, currentSortOrder);
            }
        }

        private void BarDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var currentSortOrder = BarDataGridView.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;

            var toBeSortOrder = currentSortOrder != SortOrder.None
                ? (currentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending)
                : SortOrder.Ascending;

            SortList((CompareType) e.ColumnIndex + 1, toBeSortOrder);
        }

        private void BarDataGridView_ClearHeaderSortGlyphs()
        {
            foreach (DataGridViewColumn column in BarDataGridView.Columns)
            {
                column.HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }
    }
}
