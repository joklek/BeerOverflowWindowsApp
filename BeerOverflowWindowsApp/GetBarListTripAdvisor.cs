using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    class GetBarListTripAdvisor
    {
        private const string key = "722778370a97413484c29f89476a8e18";
        private const string TripAdvisorAPILink = "http://api.tripadvisor.com/api/partner/2.0/location_mapper/{0},{1}?key={2}-mapper&category=restaurants";

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            try
            {
                var result = GetBarData(latitude, longitude, radius);
                barList = PlacesApiQueryResponseToBars(result, radius);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return barList;
        }

        private TripAdvisorDataModel.PlacesResponse GetBarData(string latitude, string longitude, string radius)
        {
            using (var client = new HttpClient())
            {
                TripAdvisorDataModel.PlacesResponse result = null;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(string.Format(TripAdvisorAPILink, latitude, longitude, key));
                    result = JsonConvert.DeserializeObject<TripAdvisorDataModel.PlacesResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<BarData> PlacesApiQueryResponseToBars(TripAdvisorDataModel.PlacesResponse resultData, string radius)
        {
            List<BarData> barList = new List<BarData>();
            foreach (var result in resultData.data)
            {
                
                if (int.Parse(result.distance) <= int.Parse(radius))
                {
                    var newBar = new BarData
                    {
                        Title = result.name
                    };
                    barList.Add(newBar);
                }
            }
            return barList;
        }
    }
}
