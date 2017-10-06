using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Globalization;

using System.Windows.Forms;

namespace BeerOverflowWindowsApp
{
    public partial class GetBarList : Form
    {
        BarRating barRating = new BarRating();
        private const double radius = 500;

        public GetBarList()
        {
            InitializeComponent();
            var location = new CurrentLocation();
            var currentLocation = location.currentLocation;
            var latitude = currentLocation.Latitude.ToString(CultureInfo.InvariantCulture);
            var longitude = currentLocation.Longitude.ToString(CultureInfo.InvariantCulture);
            latitudeBox.Text = latitude;
            longitudeBox.Text = longitude;
            radiusTextBox.Text = radius.ToString(CultureInfo.InvariantCulture);
        }

        private void Go_Click(object sender, EventArgs e)
        {
            ProgressBar.Value = 0;
            ProgressBar.Visible = true;
            try
            {
                var result = new BarDataModel {BarsList = new List<BarData>()};
                result.CombineLists(new GetBarListGoogle().GetBarsAround(GetLatitude(), GetLongitude(), GetRadius()));
                ProgressBar.Increment(25);
                result.CombineLists(
                    new GetBarListFourSquare().GetBarsAround(GetLatitude(), GetLongitude(), GetRadius()));
                ProgressBar.Increment(25);
                result.CombineLists(new GetBarListFacebook().GetBarsAround(GetLatitude(), GetLongitude(), GetRadius()));
                ProgressBar.Increment(25);
                result.CombineLists(
                    new GetBarListTripAdvisor().GetBarsAround(GetLatitude(), GetLongitude(), GetRadius()));
                ProgressBar.Increment(25);
                DisplayData(result.BarsList);
                barRating.AddBars(result.BarsList);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong with the message: " + exception.Message);
            }
            finally
            {
                ProgressBar.Visible = false;
            }
        }

        // Clears the display first, then adds text to display
        private void DisplayData(IEnumerable<BarData> resultData)
        {
            resultTextBox.Clear();
            foreach (var result in resultData)
            {
                resultTextBox.AppendText(result.Title);
                resultTextBox.AppendText(Environment.NewLine);
            }
        }

        private string GetLatitude ()
        {
            return latitudeBox.Text;
        }

        private string GetLongitude ()
        {
            return longitudeBox.Text;
        }

        private string GetRadius ()
        {
            return radiusTextBox.Text;
        }

        private void RateBarsButton_Click(object sender, EventArgs e)
        {
            var ratingsForm = new BarRatingsForm();
            ratingsForm.Show();
        }
    }
}
