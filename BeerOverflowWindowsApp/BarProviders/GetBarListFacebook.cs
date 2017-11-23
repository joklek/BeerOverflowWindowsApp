using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.FacebookDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListFacebook : IBeerable
    {
        public string ProviderName { get; } = "Facebook";
        private static readonly string _apiLink = ConfigurationManager.AppSettings["FacebookAPILink"];
        private static readonly string _accessToken = ConfigurationManager.AppSettings["FacebookAccessToken"];
        private static readonly string _category = ConfigurationManager.AppSettings["FacebookCategoryID"];
        private static readonly string _allowedCategories = ConfigurationManager.AppSettings["FacebookAllowedCategoryStrings"];
        private static readonly string _bannedCategories = ConfigurationManager.AppSettings["FacebookBannedCategoryStrings"];
        private static readonly string _requestedFields = ConfigurationManager.AppSettings["FacebookRequestedFields"];
        private readonly IHttpFetcher _fetcher;

        public GetBarListFacebook(IHttpFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private IEnumerable<Place> GetBarData(string latitude, string longitude, string radius)
        {
            var link = string.Format(_apiLink, _accessToken, latitude, longitude, radius, _requestedFields, _category);
            var barList = FetcherAndDeserializer.FetchAndDeserialize<PlacesResponse>(link, _fetcher).data;
            return barList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private async Task<List<Place>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var link = string.Format(_apiLink, _accessToken, latitude, longitude, radius, _requestedFields, _category);
            var deserialized = await FetcherAndDeserializer.FetchAndDeserializeAsync<PlacesResponse>(link, _fetcher);
            var barList = deserialized.data;
            return barList;
        }

        private List<BarData> PlaceListToBarList(IEnumerable<Place> resultData)
        {
            var allowedCategories = string.Join(",", _allowedCategories.Split(',').ToList().SelectMany(category => category.Split('|')).Where((c, i) => i % 2 == 0));
            var barList = new List<BarData>();
            foreach (var result in resultData)
            {
                if (result.restaurant_specialties != null && result.restaurant_specialties.drinks != 1) continue;
                var barCategories = result.category_list.Select(category => category.name).ToList();

                if (!HasCategories(barCategories, allowedCategories)) continue;
                if (HasCategories(barCategories, _bannedCategories)) continue;
                var newBar = PlaceToBar(result);
                barList.Add(newBar);
            }
            return barList;
        }

        private static BarData PlaceToBar(Place place)
        {
            return new BarData
            {
                Title = place.name,
                BarId = place.name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(place),
                Latitude = place.location.latitude,
                Longitude = place.location.longitude,
            };
        }

        private static CategoryTypes CollectCategories(Place place)
        {
            var placeCategories = CategoryTypes.None;
            var listOfCategories = _allowedCategories.Split(',').Select
                (category => category.Split('|')).Select
                (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                .ToList();

            foreach (var category in place.category_list)
            {
                var foundCategory = listOfCategories.FirstOrDefault(x => category.name.ToLower().Contains(x.NameFromProvider.ToLower()));
                CategoryTypes foundCategoryEnum;
                if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out foundCategoryEnum))
                {
                    placeCategories |= foundCategoryEnum;
                }
            }
            return placeCategories;
        }

        private static bool HasCategories(IEnumerable<string> categories, string bannedCategoriesInString)
        {
            var allowedCategoryList = bannedCategoriesInString.Split(',');
            foreach (var category in categories)
            {
                if (allowedCategoryList.Any(allowed => category.ToLower().Contains(allowed.ToLower())))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
