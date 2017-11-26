using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.BarProviders;
using WebApi.Database;
using WebApi.DataModels;

namespace WebApi.Utilities
{
    public static class BarFetcher
    {
        private static readonly List<object> _providerList = new List<object>
        {
            new GetBarListGoogle(new JsonFetcher()),
            new GetBarListFourSquare(new JsonFetcher()),
            new GetBarListFacebook(new JsonFetcher()),
            new GetBarListTripAdvisor(new JsonFetcher())
        };


        public static async Task<BarDataModel> RequestBarsAroundCoords(LocationRequestModel locationRequest)
        {
            return await RequestBarsAroundCoords(locationRequest.Latitude, locationRequest.Longitude, locationRequest.Radius, locationRequest.User);
        }

        public static async Task<BarDataModel> RequestBarsAroundCoords(double latitude, double longitude, double radius, User user)
        {
            var result = new BarDataModel();
            var barsFromProvider = new List<BarData>();
            var failedToConnectCounter = 0;
            var providerCount = _providerList.Count;
            foreach (IBeerable provider in _providerList)
            {
                try
                {
                    barsFromProvider = CollectBarsFromProvider(provider, latitude, longitude, radius); // TODO ASYNC
                }
                catch (Exception e)
                {
                    throw;
                }
                /*catch (HttpRequestException)
                {
                    if (++failedToConnectCounter == providerCount)
                    {
                        // TODO: FORM RESPONSE
                        //MessageBox.Show("Check your internet connection, it seems to be down.");
                    }
                }
                catch (WebException)
                {
                    // TODO: FORM RESPONSE
                    //MessageBox.Show("Failed connecting to: " + provider.ProviderName);
                }*/
                result.AddRange(barsFromProvider);
            }
            result.RemoveDuplicates();
            result.RemoveBarsOutsideRadius(radius);
            var dbManager = new DatabaseManager();
            result = (BarDataModel) dbManager.GetAllBarData(result, user);
            return result;
        }

        private static List<BarData> CollectBarsFromProvider(IBeerable provider,
            double latitude, double longitude, double radius)
        {
            List<BarData> response = null;
            try
            {
                response = provider.GetBarsAround(latitude, longitude, radius);
            }
            catch (NotImplementedException)
            {
                //response = await provider.GetBarsAroundAsync(latitude, longitude, radius); // TODO: Change places sync and async methods
            }
            return response;
        }
    }
}