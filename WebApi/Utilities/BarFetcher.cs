using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
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
            var failedToConnectCounter = 0;
            var providerCount = _providerList.Count;
            foreach (IBeerable provider in _providerList)
            {
                try
                {
                    var barsFromProvider = await CollectBarsFromProvider(provider, latitude, longitude, radius);
                    result.AddRange(barsFromProvider);
                }
                catch (HttpRequestException)
                {
                    if (++failedToConnectCounter == providerCount)
                    {
                        throw;
                    }
                }
                catch (WebException)
                {
                    // Provider is down, lets just ignore it
                }
            }
            result.RemoveDuplicates();
            result.RemoveBarsOutsideRadius(radius);
            var dbManager = new DatabaseManager();
            result = (BarDataModel) dbManager.GetAllBarData(result, user);
            return result;
        }

        private static async Task<List<BarData>> CollectBarsFromProvider(IBeerable provider,
            double latitude, double longitude, double radius)
        {
            try
            {
                return await provider.GetBarsAroundAsync(latitude, longitude, radius);
            }
            catch (NotImplementedException)
            {
                return provider.GetBarsAround(latitude, longitude, radius);
            }
        }
    }
}