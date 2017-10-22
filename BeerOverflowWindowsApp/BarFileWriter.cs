using System.Configuration;
using BeerOverflowWindowsApp.DataModels;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        private static string _filePath = ConfigurationManager.AppSettings["filePath"];

        public static void SaveData(BarDataModel localBarData)
        {
            var barsInFile = BarFileReader.GetAllBarData();
            foreach (BarData localBar in localBarData)
            {
                int barInFileIndex = barsInFile.FindIndex(x => x.Title == localBar.Title);
                if (barInFileIndex != -1)
                {
                    BarData barOccurenceInFile = barsInFile[barInFileIndex];
                    if (barOccurenceInFile.Ratings == null)
                    {
                        barOccurenceInFile.Ratings = new List<int>();
                    }
                    if (localBar.Ratings != null && !barOccurenceInFile.Ratings.SequenceEqual(localBar.Ratings))
                    {
                        barOccurenceInFile.Ratings = localBar.Ratings;
                    }
                }
                else
                {
                    barsInFile.Add(localBar);
                }
            }
            List<string> barsDataJson = new List<string>();
            foreach (var bar in barsInFile)
            {

                barsDataJson.Add(JsonConvert.SerializeObject(bar));
                
            }
            File.WriteAllLines(_filePath, barsDataJson);
        }
    }
}
