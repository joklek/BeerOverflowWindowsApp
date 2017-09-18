using System.Collections.Generic;
using System.Threading.Tasks;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;

namespace BeerOverflowWindowsApp
{
    class GetBarListFourSquare : IBeerable
    {
        string FourSquare_clientId = "XN5J1TJ5RREJR1RVFBT2NLEN5HJXQU1VZYL2MC21MJSTCNRC";
        string FourSquare_clientSecret = "YWHT33SLUDBU4LD4YDHHE3SKNUFCGOIIZPXRYLTE1QLREF3M";

        public async Task<List<Bar>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            List<Bar> barList = null;
            var result = GetBarData(latitude, longitude, radius);
            barList = VenueListToBars(result);
            return barList;
        }

        private List<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            SharpSquare sharpSquare = new SharpSquare(FourSquare_clientId, FourSquare_clientSecret);

            // let's build the query
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                // Parameter info from https://developer.foursquare.com/docs/venues/search
                { "ll", latitude + "," + longitude }, // Coords
                { "radius", radius },
                { "categoryId", "4d4b7105d754a06376d81259" } // "Nightlife Spot" CategoryId from https://developer.foursquare.com/categorytree
            };

            return sharpSquare.SearchVenues(parameters);
        }

        private List<Bar> VenueListToBars (List<Venue> resultData)
        {
            List<Bar> barList = new List<Bar>();
            Bar newBar;
            foreach (var result in resultData)
            {
                newBar = new Bar(result.name, "FourSquareAPI");
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
