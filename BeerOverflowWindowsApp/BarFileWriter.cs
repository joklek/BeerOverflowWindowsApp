using BeerOverflowWindowsApp.DataModels;
using System.IO;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        private static string _filePath = System.Configuration.ConfigurationManager.AppSettings["filePath"];

        public static void SaveData(BarDataModel barData)
        {
            var barsDataJson = JsonConvert.SerializeObject(barData);
            File.WriteAllText(_filePath, barsDataJson);
        }
    }
}
