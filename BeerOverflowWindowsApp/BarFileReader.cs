using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BeerOverflowWindowsApp
{
    static class BarFileReader
    {
        private static readonly string _filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

        public static BarDataModel GetAllBarData()
        {
            if (File.Exists(_filePath))
            {
                var barsData = File.ReadAllText(_filePath);
                var result = JsonConvert.DeserializeObject<BarDataModel>(barsData);
                return result;
            }
            else { return new BarDataModel { BarsList = new List<BarData> { } }; }
        }

        public static List<int> GetBarRatings(BarData bar)
        {
            var barList = GetAllBarData();
            var barInDatabase = barList.BarsList.Find(x => x.Title == bar.Title);
            var barsRatings = barInDatabase?.Ratings;
            return barsRatings;
        }
    }
}
