using System.Collections.Generic;
using System.Device.Location;

namespace BeerOverflowWindowsApp.DataModels
{
    public class BarData
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
        public GeoCoordinate Location { get; set; }
        //public double Latitude { get; set; }
        //public double Longitude { get; set; }
    }
}