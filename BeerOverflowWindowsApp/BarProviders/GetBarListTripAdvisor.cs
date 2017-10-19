using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using BeerOverflowWindowsApp.DataModels;
using static BeerOverflowWindowsApp.DataModels.TripAdvisorDataModel;

namespace BeerOverflowWindowsApp
{
    class GetBarListTripAdvisor : IBeerable
    {
        private static readonly string _accessKey = ConfigurationManager.AppSettings["TripAdvisorAccessKey"];
        private static readonly string _mapperLink = ConfigurationManager.AppSettings["TripAdvisorMapperLink"];
        private static readonly string _locationApiLink = ConfigurationManager.AppSettings["TripAdvisorLocationAPILink"];
        private static readonly string _restaurantCategory = ConfigurationManager.AppSettings["TripAdvisorRestaurantsCode"];
        private static readonly string _attractionsCategory = ConfigurationManager.AppSettings["TripAdvisorAttractionsCode"];

        private readonly string _applicableGroupsString =
            ConfigurationManager.AppSettings["TripAdvisorApplicableGroups"];

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            try
            {
                var result = GetBarData(latitude, longitude);
                barList = ApiQueryResponseToBars(result, radius);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return barList;
        }

        private PlacesResponse GetBarData(string latitude, string longitude)
        {
            using (var client = new HttpClient())
            {
                PlacesResponse result = null;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(string.Format(_mapperLink, latitude, longitude, _accessKey, _restaurantCategory));
                    result = JsonConvert.DeserializeObject<PlacesResponse>(response);

                    response = webClient.DownloadString(string.Format(_mapperLink, latitude, longitude, _accessKey, _attractionsCategory));
                    result.data.AddRange(JsonConvert.DeserializeObject<PlacesResponse>(response).data);

                    FetchLocations(result);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<BarData> ApiQueryResponseToBars(PlacesResponse resultData, string radius)
        {
            List<string> groupList = _applicableGroupsString.Split(',').ToList();

            var barList = new List<BarData>();
            foreach (var result in resultData.data)
            {
                var distanceMeters = ConvertMilesToMeters(double.Parse(result.distance, CultureInfo.InvariantCulture));
                if ((int) distanceMeters <= int.Parse(radius))
                {
                    switch (result.locationResponse.category.name)
                    {
                        case "attraction":
                            if (result.locationResponse.groups != null)
                            {
                                if (result.locationResponse.groups.Exists(x => groupList.Contains(x.name)))
                                {
                                    addTripAdvisorPlaceToBars(barList, result);
                                }
                            }
                            break;
                        case "restaurant":
                            if (result.locationResponse.subcategory == null ||
                                result.locationResponse.subcategory.Exists(x => x.name == "sit_down"))
                            {
                                addTripAdvisorPlaceToBars(barList, result);
                            }
                            break;
                    }
                }
            }
            return barList;
        }

        void addTripAdvisorPlaceToBars(List<BarData> barList, Datum place)
        {
            var newBar = new BarData
            {
                Title = place.name,
                Latitude = double.Parse(place.locationResponse.latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(place.locationResponse.longitude, CultureInfo.InvariantCulture)
            };
            barList.Add(newBar);
        }

        private static double ConvertMilesToMeters(double miles)
        {
            return miles * 1.609344 * 1000;
        }

        private void GetLocationForPlace(Datum place)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    var webClient = new WebClient();
                    var response =
                        webClient.DownloadString(string.Format(_locationApiLink, place.location_id, _accessKey));
                    var result = JsonConvert.DeserializeObject<LocationResponse>(response);
                    place.locationResponse = result;
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        private void FetchLocations(PlacesResponse result)
        {
            foreach (var place in result.data)
            {
                GetLocationForPlace(place);
            }
        }
    }
}
