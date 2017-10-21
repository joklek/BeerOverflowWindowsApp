using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Device.Location;
using System.Configuration;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace BeerOverflowWindowsApp
{
    public partial class MainWindow : Form
    {
        private BarRating _barRating = null;
        private BarData _selectedBar = null;
        private readonly string _defaultRadius = ConfigurationManager.AppSettings["defaultRadius"];

        public MainWindow()
        {
            InitializeComponent();
            _barRating = new BarRating();
            var location = new CurrentLocation();
            var currentLocation = location.currentLocation;
            var latitude = currentLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = currentLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            LatitudeTextBox.Text = latitude;
            LongitudeTextBox.Text = longitude;
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
            BarDataGridView.ClearSelection();
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if (!LatitudeTextIsCorrect() || !LongitudeTextIsCorrect() || !RadiusTextIsCorrect())
            {
                MessageBox.Show("Please enter correct required data. Erroneus data is painted red.");
            }
            else
            {
                _selectedBar = null;
                var currentProgressValue = 0;
                GoButton.Enabled = false;

                InitiateProgressBars();
                UpdateProgressBars(currentProgressValue);

                var result = new BarDataModel ();
                CollectBarsFromProvider(new GetBarListGoogle(), result, GetLatitude(), GetLongitude(), GetRadius());
                currentProgressValue += 25;
                UpdateProgressBars(currentProgressValue);

                CollectBarsFromProvider(new GetBarListFourSquare(), result, GetLatitude(), GetLongitude(), GetRadius());
                currentProgressValue += 25;
                UpdateProgressBars(currentProgressValue);

                CollectBarsFromProvider(new GetBarListFacebook(), result, GetLatitude(), GetLongitude(), GetRadius());
                currentProgressValue += 25;
                UpdateProgressBars(currentProgressValue);

                CollectBarsFromProvider(new GetBarListTripAdvisor(), result, GetLatitude(), GetLongitude(), GetRadius());
                currentProgressValue += 25;
                UpdateProgressBars(currentProgressValue);
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
            progressBar.Value = currentValue;
        }

        private void CollectBarsFromProvider(IBeerable provider, BarDataModel barList,
            string latitude, string longitude, string radius)
        {
            try
            {
                barList.CombineLists(provider.GetBarsAround(latitude, longitude, radius));
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
