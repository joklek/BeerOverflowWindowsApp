﻿using BeerOverflowWindowsApp.DataModels;
using System.IO;
using Newtonsoft.Json;

namespace BeerOverflowWindowsApp
{
    static class BarFileWriter
    {
        private const string filePath = @".\barsData.txt";

        static public void SaveData(BarDataModel barData)
        {
            var barsDataJson = JsonConvert.SerializeObject(barData);
            File.WriteAllText(filePath, barsDataJson);
        }
    }
}
