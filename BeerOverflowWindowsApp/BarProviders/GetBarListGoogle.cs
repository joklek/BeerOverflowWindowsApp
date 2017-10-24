using System;
using System.Net.Http;
using Newtonsoft.Json;
using static BeerOverflowWindowsApp.DataModels.GoogleDataModel;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    class GetBarListGoogle : IBeerable
    {
        private static readonly string _googleApiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        private static readonly string _googleApiLink = ConfigurationManager.AppSettings["GoogleAPILink"] + _googleApiKey;

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
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
                    var webClient = new WebClient {Encoding = Encoding.UTF8};
                    var response = webClient.DownloadString(string.Format(_googleApiLink, latitude, longitude, radius));
                    result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<BarData> PlacesApiQueryResponseToBars (PlacesApiQueryResponse resultData)
        {
            var barList = new List<BarData>();
            foreach (var result in resultData.Results)
            {
                var newBar = new BarData
                {
                    Title = result.Name,
                    Latitude = result.Geometry.Location.Lat,
                    Longitude = result.Geometry.Location.Lng
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
