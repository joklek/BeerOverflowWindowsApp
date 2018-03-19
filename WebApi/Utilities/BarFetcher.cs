using System;
using System.Collections.Generic;
using System.Linq;
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
            return await RequestBarsAroundCoords(locationRequest.Latitude, locationRequest.Longitude, locationRequest.Radius);
        }

        public static async Task<BarDataModel> RequestBarsAroundCoords(double latitude, double longitude, double radius)
        {
            var result = new BarDataModel();
            var failedToConnectCounter = 0;
            var providerCount = _providerList.Count;
            foreach (IBeerable provider in _providerList)
            {
                try
                {
                    var barsFromProvider = await CollectBarsFromProvider(provider, latitude, longitude, radius);
                    if (barsFromProvider != null)
                    {
                        result.AddRange(barsFromProvider);
                    }
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
                    Console.Write("Provider \"{0}\" is down\n", provider.ProviderName);
                    // Provider is down, lets just ignore it
                }
            }
            var dbManager = new DatabaseManager();
            dbManager.SaveBars(result);
            result.AddRange(dbManager.GetAllBarData(result.Select(x => x.BarId)));
            result.RemoveBarsOutsideRadius(radius);
            result.RemoveDuplicates();
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