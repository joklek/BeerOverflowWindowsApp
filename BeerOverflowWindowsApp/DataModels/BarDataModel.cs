using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    class BarDataModel
    {
        public List<BarData> BarsList { get; set; }
    }
    class BarData
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
    }
}