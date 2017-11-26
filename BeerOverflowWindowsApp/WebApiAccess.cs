using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowWindowsApp
{
    static class WebApiAccess
    {
        public static User defaultUser = new User { Username = "Jonas" };

        private static readonly string _baseAddress = ConfigurationManager.AppSettings["BaseUri"];

        public static BarData GetBarRatings(BarData bar)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarModel { Bar = bar, User = defaultUser});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetBarRatings", content);
                var responseResult = response.Result.Content.ReadAsStringAsync();  // TODO error handling
                var result = JsonConvert.DeserializeObject<BarData>(responseResult.Result);
                bar.AvgRating = result.AvgRating;
                bar.UserRating = result.UserRating;
                return bar;
            }
        }

        public static List<BarData> GetAllBarData(List<BarData> localBars)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarsModel { Bars = localBars, User = defaultUser});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetAllBarData", content); // TODO error handling
                var responseResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BarData>>(responseResult.Result);
                return result;
            }
        }

        public static List<BarData> GetBarsAround(double latitude, double longitude, double radius)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new LocationRequestModel() { Latitude = latitude, Longitude = longitude, Radius = radius, User = defaultUser });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/GetBarsAround", content); // TODO error handling
                var responseResult = response.Result.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BarData>>(responseResult.Result);
                return result;
            }
        }

        public static void SaveBarRating(BarData barToRate, int rating)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new RatingModel{Bar = barToRate, Rating = rating, User = defaultUser});
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("Api/Data/SaveBarRating", content).Result;
            }
        }

        public static async Task<BarData> GetBarRatingsAsync(BarData bar)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarModel { Bar = bar, User = defaultUser });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Api/Data/GetBarRatings", content);
                var responseResult = await response.Content.ReadAsStringAsync(); // TODO error handling
                var result = JsonConvert.DeserializeObject<BarData>(responseResult);
                bar.AvgRating = result.AvgRating;
                bar.UserRating = result.UserRating;
                return bar;
            }
        }

        public static async Task<List<BarData>> GetAllBarDataAsync(List<BarData> localBars)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new UserAndBarsModel { Bars = localBars, User = defaultUser });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Api/Data/GetAllBarData", content); // TODO error handling
                var responseResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BarData>>(responseResult);
                return result;
            }
        }

        public static async Task<List<BarData>> GetBarsAroundAsync(double latitude, double longitude, double radius)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new LocationRequestModel() { Latitude = latitude, Longitude = longitude, Radius = radius, User = defaultUser });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Api/Data/GetBarsAround", content); 
                var responseResult = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<BarData>>(responseResult);
                return result;
            }
        }

        public static async Task SaveBarRatingAsync(BarData barToRate, int rating)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseAddress);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(new RatingModel { Bar = barToRate, Rating = rating, User = defaultUser });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("Api/Data/SaveBarRating", content);
            }
        }
    }
}
