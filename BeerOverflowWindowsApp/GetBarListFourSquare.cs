using System.Collections.Generic;
using FourSquare.SharpSquare.Core;
using FourSquare.SharpSquare.Entities;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    class GetBarListFourSquare
    {
        static string clientId = System.Configuration.ConfigurationManager.AppSettings["clientId"];
        static string clientSecret = System.Configuration.ConfigurationManager.AppSettings["clientSecret"];
        static string categoryIdBar = System.Configuration.ConfigurationManager.AppSettings["categoryIdBar"];

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
                { "ll", latitude + "," + longitude }, // Coords
                { "radius", radius },
                { "categoryId", categoryIdBar } 
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
