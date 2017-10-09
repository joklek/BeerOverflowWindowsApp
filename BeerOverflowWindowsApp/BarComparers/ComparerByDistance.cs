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
            var coord1 = new GeoCoordinate(barData1.Latitude, barData1.Longitude);
            var coord2 = new GeoCoordinate(barData2.Latitude, barData2.Longitude);
            var location = new CurrentLocation();
            var coordStartingLocation = location.currentLocation;
            var distance1 = coord1.GetDistanceTo(coordStartingLocation);
            var distance2 = coord2.GetDistanceTo(coordStartingLocation);
            if (distance1 == distance2)
            {
                result = 0;
            }
            else if (distance1 < distance2)
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
