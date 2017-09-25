using System.Collections.Generic;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    class GetBarListFourSquare
    {
        string clientId = "XN5J1TJ5RREJR1RVFBT2NLEN5HJXQU1VZYL2MC21MJSTCNRC";
        string clientSecret = "YWHT33SLUDBU4LD4YDHHE3SKNUFCGOIIZPXRYLTE1QLREF3M";

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            List<BarData> barList = null;
            var result = GetBarData(latitude, longitude, radius);
            barList = VenueListToBars(result);
            return barList;
        }

        private List<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            SharpSquare sharpSquare = new SharpSquare(clientId, clientSecret);

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

        private List<BarData> VenueListToBars (List<Venue> resultData)
        {
            List<BarData> barList = new List<BarData>();
            BarData newBar;
            foreach (var result in resultData)
            {
                newBar = new BarData
                {
                    Title = result.name
                };
                barList.Add(newBar);
            }
            return barList;
        }
    }
}
