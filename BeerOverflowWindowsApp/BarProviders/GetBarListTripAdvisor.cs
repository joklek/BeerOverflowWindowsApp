using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.TripAdvisorDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListTripAdvisor : IBeerable
    {
        public string ProviderName { get; } = "TripAdvisor";
        private static readonly string _accessKey = ConfigurationManager.AppSettings["TripAdvisorAccessKey"];
        private static readonly string _mapperLink = ConfigurationManager.AppSettings["TripAdvisorMapperLink"];
        private static readonly string _locationApiLink = ConfigurationManager.AppSettings["TripAdvisorLocationAPILink"];
        private static readonly string _categoryListString = ConfigurationManager.AppSettings["TripAdvisorCategories"];
        private static readonly string _applicableGroupsString = ConfigurationManager.AppSettings["TripAdvisorApplicableGroups"];
        private static readonly string _applicableGroupCategories = ConfigurationManager.AppSettings["TripAdvisorApplicableGroupCategories"];
        private readonly IHttpFetcher _fetcher;

        public GetBarListTripAdvisor(IHttpFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude);
            RemovePlacesOutsideRadius(placeList, radius);
            FetchLocations(placeList);
            RemoveUnneededPlaces(placeList);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private List<PlaceInfo> GetBarData(string latitude, string longitude)
        {
            var categories = _categoryListString.Split(',');
            var placeList =  new List<PlaceInfo>();
            foreach (var category in categories)
            {
                var link = string.Format(_mapperLink, latitude, longitude, _accessKey, category);
                placeList.AddRange(FetcherAndDeserializer.FetchAndDeserialize<PlacesResponse>(link, _fetcher).data);
            }
            return placeList;
        }

        private void FetchLocations(IEnumerable<PlaceInfo> placeList)
        {
            foreach (var place in placeList)
            {
                place.locationResponse = GetLocationForPlace(place);
            }
        }

        private LocationResponse GetLocationForPlace(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var placeLocation = FetcherAndDeserializer.FetchAndDeserialize<LocationResponse>(link, _fetcher);
            return placeLocation;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude);
            RemovePlacesOutsideRadius(placeList, radius);
            await FetchLocationsAsync(placeList);
            RemoveUnneededPlaces(placeList);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private async Task<List<PlaceInfo>> GetBarDataAsync(string latitude, string longitude)
        {
            var categories = _categoryListString.Split(',');
            var placeList = new List<PlaceInfo>();
            foreach (var category in categories)
            {
                var link = string.Format(_mapperLink, latitude, longitude, _accessKey, category);
                var deserializedResponse = await FetcherAndDeserializer.FetchAndDeserializeAsync<PlacesResponse>(link, _fetcher);
                placeList.AddRange(deserializedResponse.data);
            }
            return placeList;
        }

        private async Task FetchLocationsAsync(IEnumerable<PlaceInfo> placeList)
        {
            foreach (var place in placeList)
            {
                place.locationResponse = await GetLocationForPlaceAsync(place);
            }
        }

        private async Task<LocationResponse> GetLocationForPlaceAsync(PlaceInfo place)
        {
            var link = string.Format(_locationApiLink, place.location_id, _accessKey);
            var placeLocation = await FetcherAndDeserializer.FetchAndDeserializeAsync<LocationResponse>(link, _fetcher);
            return placeLocation;
        }

        private List<BarData> PlaceListToBarList(IEnumerable<PlaceInfo> placeList)
        {
            return placeList.Select(PlaceToBar).ToList();
        }

        private void RemovePlacesOutsideRadius(List<PlaceInfo> placeList, string radius)
        {
            placeList.RemoveAll(x =>
                ConvertMilesToMeters(x.distance) > double.Parse(radius, CultureInfo.InvariantCulture));
        }

        private void RemoveUnneededPlaces(List<PlaceInfo> placeList)
        {
            placeList.RemoveAll(x => !IsAValidAttraction(x) && !IsAValidRestaurant(x));
        }

        private static BarData PlaceToBar(PlaceInfo place)
        {
            return new BarData
            {
                Title = place.name,
                BarId = place.name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(place),
                Latitude = double.Parse(place.locationResponse.latitude, CultureInfo.InvariantCulture),
                Longitude = double.Parse(place.locationResponse.longitude, CultureInfo.InvariantCulture),
            };
        }

        private static CategoryTypes CollectCategories(PlaceInfo place)
        {
            var placeCategories = CategoryTypes.None;
            
            if (IsAValidAttraction(place))
            {
                var placeCategoryList = place.locationResponse.groups.SelectMany(group => group.categories);
                // strings are formated like this "VendorCatName1|catName1,VendorCatName2|catName2,..."
                // So first split at the ',': "VendorCatName1|catName1" , "VendorCatName2|catName2", ...
                // then split at '|': "VendorCatName1", "catName1", "VendorCatName2", "catName2", ...
                var listOfCategories = _applicableGroupCategories.Split(',').Select
                    (category => category.Split('|')).Select
                    (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                    .ToList();
                foreach (var category in placeCategoryList)
                {
                    var foundCategory = listOfCategories.FirstOrDefault(x => x.NameFromProvider == category.name);
                    CategoryTypes foundCategoryEnum;
                    if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out foundCategoryEnum))
                    {
                        placeCategories |= foundCategoryEnum;
                    }
                }
            }
            else if (IsAValidRestaurant(place))
            {
                placeCategories |= CategoryTypes.Restaurant;
            }
            return placeCategories;
        }

        private static bool IsAValidAttraction(PlaceInfo place)
        {
            var allowedGroupList = _applicableGroupsString.Split(',').ToList();
            var allowedGroupCategoryList = _applicableGroupCategories.Split(',').ToList();

            return place.locationResponse.groups != null &&
                   place.locationResponse.groups.Exists(x => allowedGroupList.Contains(x.name)) &&
                   place.locationResponse.groups.Any(group => group.categories.Exists(x => allowedGroupCategoryList.Contains(x.name)));
        }

        private static bool IsAValidRestaurant(PlaceInfo place)
        {
            return place.locationResponse.category.name == "restaurant" &&
                   place.locationResponse.subcategory.Exists(x => x.name == "sit_down");
        }

        private static double ConvertMilesToMeters(string miles)
        {
            return double.Parse(miles, CultureInfo.InvariantCulture) * 1.609344 * 1000;
        }
    }
}
