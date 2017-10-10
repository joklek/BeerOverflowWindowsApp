using BeerOverflowWindowsApp.DataModels;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        private static string _filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

        public static void SaveData(BarDataModel barData)
        {
            var barsInFile = BarFileReader.GetAllBarData();
            foreach (var bar in barData.BarsList)
            {
                var barInListIndex = barsInFile.BarsList.FindIndex(x => x.Title == bar.Title);
                if (barInListIndex != -1)
                {
                    var barOccurenceInFile = barsInFile.BarsList[barInListIndex];
                    if (barOccurenceInFile.Ratings == null
                        || barsInFile.BarsList[barInListIndex].Ratings.SequenceEqual(bar.Ratings))
                    {
                        barsInFile.BarsList[barInListIndex].Ratings = bar.Ratings;
                    }
                }
                else
                {
                    barsInFile.BarsList.Add(bar);
                }
            }
            var barsDataJson = JsonConvert.SerializeObject(barData);
            File.WriteAllText(_filePath, barsDataJson);
        }
    }
}
