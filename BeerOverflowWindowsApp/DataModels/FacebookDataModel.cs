using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    public class FacebookDataModel
    {
        public class Location
        {
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string zip { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Datum
        {
            public string name { get; set; }
            public Location location { get; set; }
            public string id { get; set; }
        }

        public class PlacesResponse
        {
            public List<Datum> data { get; set; }
        }
    }
}