using BeerOverflowWindowsApp.DataModels;
using System.IO;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    class BarFileWriter : IWritable
    {
        private const string filePath = @".\barsData.txt";

        public void SaveData(BarDataModel barData)
        {
            var barsDataJson = JsonConvert.SerializeObject(barData);
            File.WriteAllText(filePath, barsDataJson);
        }
    }
}
