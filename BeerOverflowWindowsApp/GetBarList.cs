using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DataModels.GeodataDataModel;

namespace BeerOverflowWindowsApp
{
    public partial class GetBarList : Form
    {
        const string GoogleAPIKey = "AIzaSyBqe4VYJPO86ui1aOtmpxapqwI3ET0ZaMY";
        const string GoogleAPILink = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type=bar&key=" + GoogleAPIKey;

        public GetBarList()
        {
            InitializeComponent();
            latitudeBox.Text = "54.684815";
            longitudeBox.Text = "25.288464";
            radiusTextBox.Text = "500";
        }

        private async void Go_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                var result = await GetBarDataAsync(latitudeBox.Text, longitudeBox.Text, radiusTextBox.Text);
                DisplayData(result);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Something went wrong with the message: " + exception.Message);
            }
        }

        private async Task<PlacesApiQueryResponse> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            using (var client = new HttpClient())
            {
                PlacesApiQueryResponse result = null;
                try
                {
                    var response = await client.GetStringAsync(string.Format(GoogleAPILink, latitude, longitude, radius));
                    result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        // Clears the display first, then adds text to display
        public void DisplayData(PlacesApiQueryResponse resultData)
        {
            resultTextBox.Clear();
            foreach (var result in resultData.Results)
            {
                resultTextBox.AppendText(result.Name);
                resultTextBox.AppendText(Environment.NewLine);
            }
        }
    }
}
