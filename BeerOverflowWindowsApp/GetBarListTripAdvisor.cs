using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Http;
using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    class GetBarListTripAdvisor
    {
        private const string key = "722778370a97413484c29f89476a8e18";
        private const string TripAdvisorMapperLink = 
            "http://api.tripadvisor.com/api/partner/2.0/location_mapper/{0},{1}?key={2}-mapper&category=restaurants";
        private const string TripAdvisorLocationAPILink =
            "http://api.tripadvisor.com/api/partner/2.0/location/{0}?key={1}";
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
                    var response = webClient.DownloadString(string.Format(TripAdvisorMapperLink, latitude, longitude, key));
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
                // Convert miles to a float and multiply by 1.609344 * 1000 = 1609, to get kilometers
                // Culture invariant set to avoid formatting errors
                var floatMiles = double.Parse(result.distance, CultureInfo.InvariantCulture) * 1.609344 * 1000;

                if ( (int) floatMiles <= int.Parse(radius))
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

        // Commented for future work, when the branch, where location getting is needed
        // This method is needed, because TripAdvisor doesn't give coords by default, you need an addition request
        /*
        private void GetLocationForPlace(TripAdvisorDataModel.Datum place)
        {
            using (var client = new HttpClient())
            {
                TripAdvisorDataModel.TripAdvisorLocation result = null;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(string.Format(TripAdvisorLocationAPILink, place.location_id ,key));
                    result = JsonConvert.DeserializeObject<TripAdvisorDataModel.TripAdvisorLocation>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }*/
    }
}
