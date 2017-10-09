using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    public partial class MainWindow : Form
    {
        private BarRating _barRating = null;
        private BarData barToRate = null;
        private readonly string defaultRadius = "150";

        public MainWindow()
        {
            InitializeComponent();
            var location = new CurrentLocation();
            var currentLocation = location.currentLocation;
            var latitude = currentLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = currentLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            LatitudeTextBox.Text = latitude;
            LongitudeTextBox.Text = longitude;
            RadiusTextBox.Text = defaultRadius;
        }

        public void ReLoadForm()
        {
            var barData = _barRating.GetBarsData();
            BarDataGridView.Rows.Clear();
            foreach (var bar in barData.BarsList)
            {
                var rating = bar.Ratings?.Average().ToString(CultureInfo.InvariantCulture) ?? "-";
                BarDataGridView.Rows.Add(bar.Title, rating);
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
                ProgressBar.Value = 0;
                ProgressBar.Visible = true;
                var result = new BarDataModel { BarsList = new List<BarData>() };

                CollectBarsFromProvider(new GetBarListGoogle(), result, GetLatitude(), GetLongitude(), GetRadius());
                ProgressBar.Increment(25);
                CollectBarsFromProvider(new GetBarListFourSquare(), result, GetLatitude(), GetLongitude(), GetRadius());
                ProgressBar.Increment(25);
                CollectBarsFromProvider(new GetBarListFacebook(), result, GetLatitude(), GetLongitude(), GetRadius());
                ProgressBar.Increment(25);
                CollectBarsFromProvider(new GetBarListTripAdvisor(), result, GetLatitude(), GetLongitude(), GetRadius());
                ProgressBar.Increment(25);
                ProgressBar.Visible = false;
                // Display
                _barRating = new BarRating();
                _barRating.AddBars(result.BarsList);
                ReLoadForm();
            }
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

        private void ButtonSortByTitle_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Title);
            ReLoadForm();
        }

        private void ButtonSortByRating_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Rating);
            ReLoadForm();
        }

        private void ButtonSortByDistance_Click(object sender, EventArgs e)
        {
            _barRating.Sort(CompareType.Distance);
            ReLoadForm();
        }

        private void manualBarRating_Click(object sender, EventArgs e)
        {
            var rating = manualBarRating.Rating;
            if (barToRate != null && rating != "" && int.TryParse(rating, out var ratingNumber))
            {
                _barRating.AddRating(barToRate ,ratingNumber);
                ReLoadForm();
            }
        }

        private void BarDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var val = BarDataGridView[0, e.RowIndex].Value.ToString();
                barToRate = _barRating.GetBarsData().BarsList.First(bar => bar.Title == val);
            }
        }

        private void LongitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!LongitudeTextIsCorrect())
            {
                LongitudeTextBox.ForeColor = Color.Red;
            }
            else { LongitudeTextBox.ResetForeColor(); }
        }

        private bool LongitudeTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(LongitudeTextBox.Text,
                @"^(-?(?:1[0-7]|[1-9])?\d(?:\.\d{0,8})?|180(?:\.0{0,8})?)$");
        }

        private void LatitudeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!LatitudeTextIsCorrect())
            {
                LatitudeTextBox.ForeColor = Color.Red;
            }
            else { LatitudeTextBox.ResetForeColor();}
        }

        private bool LatitudeTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(LatitudeTextBox.Text,
                @"^(-?[1-8]?\d(?:\.\d{0,8})?|90(?:\.0{0,8})?)$");
        }

        private void RadiusTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!RadiusTextIsCorrect())
            {
                RadiusTextBox.ForeColor = Color.Red;
            }
            else { RadiusTextBox.ResetForeColor(); }
        }

        private bool RadiusTextIsCorrect()
        {
            return System.Text.RegularExpressions.Regex.IsMatch(RadiusTextBox.Text,
                @"^[0-9]{0,3}$");
        }
    }
}
