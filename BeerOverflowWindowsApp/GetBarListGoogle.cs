using System;
using System.Net.Http;
using Newtonsoft.Json;
using static DataModels.GeodataDataModel;
using System.Collections.Generic;
using System.Net;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    class GetBarListGoogle
    {
        static string GoogleAPIKey = System.Configuration.ConfigurationManager.AppSettings["GoogleAPIKey"];
        static string GoogleAPILink = System.Configuration.ConfigurationManager.AppSettings["GoogleAPILink"] + GoogleAPIKey;

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

        private List<BarData> PlacesApiQueryResponseToBars (PlacesApiQueryResponse resultData)
        {
            List<BarData> barList = new List<BarData>();
            BarData newBar;
            foreach (var result in resultData.Results)
            {
                newBar = new BarData
                {
                    Title = result.Name
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
