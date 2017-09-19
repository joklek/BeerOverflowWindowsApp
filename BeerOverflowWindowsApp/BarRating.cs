using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    class BarRating
    {
        string filePath =  @".\barsData.txt";
        BarDataModel barsData = null;

        public BarRating()
        {
            barsData = GetBarsDataFromFile();
        }

        BarDataModel GetBarsDataFromFile()
        {
            if (File.Exists(filePath))
            {
                string barsData = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<BarDataModel>(barsData);
                return result;
            }
            else { return new BarDataModel { BarsList = new List<BarData> { } }; }
        }

        void SaveData()
        {
            var barsDataJson = JsonConvert.SerializeObject(barsData);
            File.WriteAllText(filePath, barsDataJson);
        }

        public void AddRating(BarData barData, int rating)
        {
            var barsCount = barsData.BarsList.Where(x => x.Title == barData.Title).Count();
            if (barsCount > 0)
            {
                var ratings = barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> { };
                barData.Ratings.Add(rating);
                barsData.BarsList.Add(barData);
            }
            SaveData();
        }

        public BarDataModel GetBarsData()
        {
            return barsData;
        }

        public void AddBars(List<Bar> barsList)
        {
            foreach (var bar in barsList)
            {
                if (barsData.BarsList.Where(x => x.Title == bar.GetName()).Count() == 0)
                {
                    barsData.BarsList.Add(new BarData { Title = bar.GetName() });
                }
            }
            SaveData();
        }
    }
}
