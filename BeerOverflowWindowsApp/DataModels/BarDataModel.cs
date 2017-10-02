using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    class BarDataModel
    {
        public List<BarData> BarsList { get; set; }
    }
    public class BarData
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}