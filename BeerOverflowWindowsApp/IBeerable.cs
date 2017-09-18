﻿using System.Collections.Generic;
using System.Threading.Tasks;
using static DataModels.GeodataDataModel;

namespace BeerOverflowWindowsApp
{
    interface IBeerable
    {
        //List<Bar> GetBarsAround(string latitude, string longitude, string radius);
        Task<List<Bar>> GetBarsAroundAsync(string latitude, string longitude, string radius);
    }
}