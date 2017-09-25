using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;

namespace BeerOverflowWindowsApp
{
    interface IBeerable
    {
        //List<Bar> GetBarsAround(string latitude, string longitude, string radius);
        List<BarData> GetBarsAround(string latitude, string longitude, string radius);
    }
}
