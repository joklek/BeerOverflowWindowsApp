using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Device.Location;
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
            var latitude = currentLocation.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            var longitude = currentLocation.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture);
            latitudeBox.Text = latitude;
            longitudeBox.Text = longitude;
            radiusTextBox.Text = radius.ToString();
        }

        private void Go_Click(object sender, EventArgs e)
        {
            try
            {
                GetBarListGoogle barListGoogle = new GetBarListGoogle();
                GetBarListFourSquare barListFourSquare = new GetBarListFourSquare();
                var result = barListGoogle.GetBarsAround(GetLatitude(), GetLongitude(), GetRadius());
                result = CombineLists(result, barListFourSquare.GetBarsAround(GetLatitude(), GetLongitude(), GetRadius()));
                DisplayData(result);
                barRating.AddBars(result);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong with the message: " + exception.Message);
            }
        }

        private List<BarData> CombineLists(List<BarData> primaryList, List<BarData> secondaryList)
        {
            var length = secondaryList.ToArray().Length;
            for (int i = 0; i < length; i++)
            {
                if (!primaryList.Contains(secondaryList[i]))
                {
                    primaryList.Add(secondaryList[i]);
                }
            }
            return primaryList;
        }

        // Clears the display first, then adds text to display
        private void DisplayData(List<BarData> resultData)
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
