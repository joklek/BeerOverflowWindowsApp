using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    class TripAdvisorDataModel
    {
        public class AddressObj
        {
            public string street1 { get; set; }
            public string street2 { get; set; }
            public string city { get; set; }
            public object state { get; set; }
            public string country { get; set; }
            public string postalcode { get; set; }
            public string address_string { get; set; }
        }

        public class Datum
        {
            public string location_id { get; set; }
            public string name { get; set; }
            public string distance { get; set; }
            public Location location { get; set; }
            public AddressObj address_obj { get; set; }
        }

        public class PlacesResponse
        {
            public List<Datum> data { get; set; }
        }

        public class Location
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }
    }
}
