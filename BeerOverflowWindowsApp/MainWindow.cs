using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Device.Location;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace BeerOverflowWindowsApp
{
    public partial class MainWindow : Form
    {
        private BarRating _barRating = null;
        private int _lastRowIndex = 0;
        private int _lastSortColumnIndex = -1;
        private BarData barToRate = null;
        private readonly string defaultRadius = "150";

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
            RadiusTextBox.Text = defaultRadius;
        }

        public void ReLoadDataGrid(bool completely = false)
        {
            if (completely)
            {
                for(var index = 0; index< BarDataGridView.Columns.Count; index++) {
                    BarDataGridView.Columns[index].HeaderCell.SortGlyphDirection = (SortOrder)0;
                }
            }            
            var barData = _barRating.BarsData;
            BarDataGridView.Rows.Clear();
            var currentLatitude = Convert.ToDouble(GetLatitude(), CultureInfo.InvariantCulture);
            var currentLongitude = Convert.ToDouble(GetLongitude(), CultureInfo.InvariantCulture);
            var currentLocation = new GeoCoordinate(currentLatitude, currentLongitude);
            foreach (var bar in barData.BarsList)
            {
                var rating = bar.Ratings?.Average().ToString("0.00") ?? "0";
                var barLocation = new GeoCoordinate(bar.Latitude, bar.Longitude);
                var distance = currentLocation.GetDistanceTo(barLocation).ToString("0");
                BarDataGridView.Rows.Add(bar.Title, rating, distance);               
            }
            if (BarDataGridView.Rows.Count > 0)
            {
                BarDataGridView.Rows[_lastRowIndex].Selected = true;               
                var val = BarDataGridView[0, _lastRowIndex].Value.ToString();
                barToRate = _barRating.BarsData.BarsList.First(bar => bar.Title == val);
            }
            
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            if (!LatitudeTextIsCorrect() || !LongitudeTextIsCorrect() || !RadiusTextIsCorrect())
            {
                MessageBox.Show("Please enter correct required data. Erroneus data is painted red");
            }
            else
            {
                var currentProgressValue = 0;

                InitiateProgressBars();
                UpdateProgressBars(currentProgressValue);

                var result = new BarDataModel { BarsList = new List<BarData>() };
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
                //_barRating = new BarRating();
                _barRating.BarsData = result;
                //_barRating.AddBars(result.BarsList);
                _barRating.ResetLastCompare();
                _lastSortColumnIndex = -1;
                SortList(CompareType.Distance);
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

        private void manualBarRating_Click(object sender, EventArgs e)
        {
            var rating = manualBarRating.Rating;
            if (barToRate != null && rating != "" && int.TryParse(rating, out var ratingNumber))
            {
                _barRating.AddRating(barToRate ,ratingNumber);
                ReLoadDataGrid();
            }
        }

        private void BarDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                _lastRowIndex = e.RowIndex;
                var val = BarDataGridView[0, e.RowIndex].Value.ToString();
                barToRate = _barRating.BarsData.BarsList.First(bar => bar.Title == val);
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

        private void SortList(CompareType sortType)
        {
            var columnIndex = (int) sortType - 1;
            _barRating.Sort(sortType);
            if (_lastSortColumnIndex == columnIndex)
            {
                BarDataGridView.Columns[columnIndex].HeaderCell.SortGlyphDirection =
                    BarDataGridView.Columns[columnIndex].HeaderCell.SortGlyphDirection == (SortOrder)1 ? (SortOrder)2 : (SortOrder)1;
            }
            else
            {
                BarDataGridView.Columns[columnIndex].HeaderCell.SortGlyphDirection = (SortOrder)1;
                if (_lastSortColumnIndex != -1)
                {
                    BarDataGridView.Columns[_lastSortColumnIndex].HeaderCell.SortGlyphDirection = (SortOrder)0;
                }
            }
            _lastSortColumnIndex = columnIndex;
            ReLoadDataGrid();
        }

        private void BarDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SortList((CompareType) e.ColumnIndex + 1);
        }
    }
}
