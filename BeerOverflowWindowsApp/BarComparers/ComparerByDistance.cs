using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Device.Location;

namespace BeerOverflowWindowsApp.BarComparers
{
    class ComparerByDistance : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            int result;
            var distance1 = barData1.DistanceToCurrentLocation;
            var distance2 = barData2.DistanceToCurrentLocation;
            if (distance1 == distance2)
            {
                result = 0;
            }
            else if (distance1 > distance2)
            {
                result = 1;
            }
            else
            {
                result = -1;
            }
            return result;
        }
    }
}
