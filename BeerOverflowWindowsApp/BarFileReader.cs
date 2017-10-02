using BeerOverflowWindowsApp.DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace BeerOverflowWindowsApp
{
    static class BarFileReader
    {
        static string filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

        static public BarDataModel ReadData()
        {
            if (File.Exists(filePath))
            {
                string barsData = File.ReadAllText(filePath);
                var result = JsonConvert.DeserializeObject<BarDataModel>(barsData);
                return result;
            }
            else { return new BarDataModel { BarsList = new List<BarData> { } }; }
        }
    }
}
