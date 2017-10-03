using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    class GetBarListFacebook : IBeerable
    {
        private const string fbAPILink =
            "https://graph.facebook.com/v2.10/search?access_token={0}&type=place&center={1}&distance={2}&fields={3}&categories=['{4}']";
        private const string fbAccessToken = "EAABpaWOJYT8BALCrZBXNm6ZAGYZBPiJ7qxpEnMYWK0XZB1I7AbHQ5tfmZBnqhHKg2kAtUk9CV0ianlZBnPwWxpgtJ5mIZB9Jb6u17dHvMDRA3H8yfqX5ON1LRpxo9ZAnR5Xv9lAepH2Ohx338ZBjiO47IqpXlV8lvkY1o9UzFVaDQWQZDZD";
        private const string Category = "FOOD_BEVERAGE";

        private List<string> fieldList = new List<string>
        {
            "location",
            "name"
        };

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            var result = GetBarData(latitude, longitude, radius);
            var barList = FacebookDataToBars(result);
            return barList;
        }

        // Lazy method of building a string of fields 
        private string GetFields()
        {
            var returned = "";
            foreach (var field in fieldList)
            {
                if (returned != "")
                {
                    returned += ",";
                }
                returned += field;
            }
            return returned;
        }

        private FacebookDataModel.PlacesResponse GetBarData(string latitude, string longitude, string radius)
        {
            using (var client = new HttpClient())
            {
                FacebookDataModel.PlacesResponse result;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(
                        string.Format(fbAPILink, fbAccessToken, latitude+","+longitude, radius, GetFields(), Category));
                    result = JsonConvert.DeserializeObject<FacebookDataModel.PlacesResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<BarData> FacebookDataToBars(FacebookDataModel.PlacesResponse resultData)
        {
            var barList = new List<BarData>();
            
            foreach (var result in resultData.data)
            {
                BarData newBar = new BarData()
                {
                    Title = result.name,
                    Latitude = result.location.latitude,
                    Longitude = result.location.longitude
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}