using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using static DataModels.GeodataDataModel;
using System.Collections.Generic;
using System.Net;

namespace BeerOverflowWindowsApp
{
    class GetBarListGoogle
    {
        private static string GoogleAPIKey = "AIzaSyBqe4VYJPO86ui1aOtmpxapqwI3ET0ZaMY";
        private static string GoogleAPILink = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={0},{1}&radius={2}&type=bar&key=" + GoogleAPIKey;

        public List<Bar> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<Bar> barList = null;
            try
            {
                var result = GetBarData(latitude, longitude, radius);
                barList = PlacesApiQueryResponseToBars(result);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return barList;
        }

        private PlacesApiQueryResponse GetBarData(string latitude, string longitude, string radius)
        {
            using (var client = new HttpClient())
            {
                PlacesApiQueryResponse result = null;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(string.Format(GoogleAPILink, latitude, longitude, radius));
                    result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<Bar> PlacesApiQueryResponseToBars (PlacesApiQueryResponse resultData)
        {
            List<Bar> barList = new List<Bar>();
            Bar newBar;
            foreach (var result in resultData.Results)
            {
                newBar = new Bar(result.Name, "GoogleAPI");
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
