using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace BeerOverflowWindowsApp
{
    static class BarFileReader
    {
        private static readonly string _filePath = ConfigurationManager.AppSettings["filePath"];

        public static BarDataModel GetAllBarData()
        {
             BarDataModel result = new BarDataModel();

            if (File.Exists(_filePath))
            {
                List<string> barsDataJson = new List<string>(File.ReadAllLines(_filePath));
                foreach(string bar in barsDataJson)
                    result.Add(JsonConvert.DeserializeObject<BarData>(bar)); 
            }
            if (result == null)
            {
                result = new BarDataModel();
            }
            return result;
        }

        public static List<int> GetBarRatings(BarData bar)
        {
            var barList = GetAllBarData();
            var barInDatabase = barList.Find(x => x.Title == bar.Title);
            var barsRatings = barInDatabase?.Ratings;
            return barsRatings;
        }
    }
}
