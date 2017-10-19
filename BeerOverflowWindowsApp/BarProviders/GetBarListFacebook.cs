using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using static BeerOverflowWindowsApp.DataModels.FacebookDataModel;

namespace BeerOverflowWindowsApp
{
    class GetBarListFacebook : IBeerable
    {
        private readonly string _fbApiLink = ConfigurationManager.AppSettings["FacebookAPILink"];
        private readonly string _fbAccessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        private readonly string _category = ConfigurationManager.AppSettings["FacebookCategoryID"];

        private List<string> fieldList = new List<string>
        {
            "location",
            "name",
            "restaurant_specialties"
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

        private PlacesResponse GetBarData(string latitude, string longitude, string radius)
        {
            using (var client = new HttpClient())
            {
                PlacesResponse result;
                try
                {
                    var webClient = new WebClient();
                    var response = webClient.DownloadString(
                        string.Format(_fbApiLink, _fbAccessToken, latitude+","+longitude, radius, GetFields(), _category));
                    result = JsonConvert.DeserializeObject<PlacesResponse>(response);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
                return result;
            }
        }

        private List<BarData> FacebookDataToBars(PlacesResponse resultData)
        {
            var barList = new List<BarData>();
            
            foreach (var result in resultData.data)
            {
                if (result.restaurant_specialties == null ||
                    result.restaurant_specialties.drinks == 1)
                {
                    var newBar = new BarData()
                    {
                        Title = result.name,
                        Latitude = result.location.latitude,
                        Longitude = result.location.longitude
                    };
                    barList.Add(newBar);
                }
            }
            return barList;
        }
    }
}