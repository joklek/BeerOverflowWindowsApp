using System.Collections.Generic;

namespace BeerOverflowWindowsApp.DataModels
{
    class TripAdvisorDataModel
    {
        public class Datum
        {
            public string distance { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string name { get; set; }
        }

        public class PlacesResponse
        {
            public List<Datum> data { get; set; }
        }
    }
}
