using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApi.DataModels;
using WebApi.Utilities;
using static WebApi.DataModels.GoogleDataModel;

namespace WebApi.BarProviders
{
    class GetBarListGoogle : IBeerable
    {
        public string ProviderName { get; } = "Google";
        private static readonly string _apiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
        private static readonly string _apiLink = ConfigurationManager.AppSettings["GoogleAPILink"];
        private static readonly string _categoryList = ConfigurationManager.AppSettings["GoogleAPICategories"];
        private readonly IHttpFetcher _fetcher;

        public GetBarListGoogle(IHttpFetcher fetcher)
        {
            _fetcher = fetcher;
        }


        public List<BarData> GetBarsAround(double latitude, double longitude, double radius)
        {
            InputDataValidator.LocationDataIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private IEnumerable<Place> GetBarData(double latitude, double longitude, double radius)
        {
            var categoryList = _categoryList.Split(',').ToList().SelectMany(category => category.Split('|')).Where((c, i) => i % 2 == 0);
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), radius.ToString(CultureInfo.InvariantCulture), category, _apiKey, CultureInfo.InvariantCulture);
                var deserialized = FetcherAndDeserializer.FetchAndDeserialize<PlacesApiQueryResponse>(link, _fetcher).Results;
                deserialized.ForEach(x => x.Category = category);
                placeList.AddRange(deserialized);
            }
            return placeList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(double latitude, double longitude, double radius)
        {
            InputDataValidator.LocationDataIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private async Task<IEnumerable<Place>> GetBarDataAsync(double latitude, double longitude, double radius)
        {
            var categoryList = _categoryList.Split(',').ToList().SelectMany(category => category.Split('|')).Where((c, i) => i % 2 == 0);
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), radius.ToString(CultureInfo.InvariantCulture), category, _apiKey, CultureInfo.InvariantCulture);
                var deserializedResponse = await FetcherAndDeserializer.FetchAndDeserializeAsync<PlacesApiQueryResponse>(link, _fetcher);
                deserializedResponse.Results.ForEach(x => x.Category = category);
                placeList.AddRange(deserializedResponse.Results);
            }
            return placeList;
        }

        private static List<BarData> PlaceListToBarList(IEnumerable<Place> placeList)
        {
            return placeList.Select(PlaceToBar).ToList();
        }

        private static BarData PlaceToBar(Place place)
        {
            var vicinitySplit = place.Vicinity.Split(',');
            var newBar = new BarData
            {
                Title = place.Name,
                BarId = place.Name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(place),
                Latitude = place.Geometry.Location.Lat,
                Longitude = place.Geometry.Location.Lng,
            };
            if (vicinitySplit.Length == 2)
            {
                newBar.StreetAddress = vicinitySplit[0].Trim();
                newBar.City = vicinitySplit[1].Trim();
            }
            else
            {
                newBar.StreetAddress = place.Vicinity?.Trim();
            }

            return newBar;
        }

        private static CategoryTypes CollectCategories(Place place)
        {
            var placeCategories = CategoryTypes.None;
            var listOfCategories = _categoryList.Split(',').Select
                (category => category.Split('|')).Select
                (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                .ToList();

            var foundCategory = listOfCategories.FirstOrDefault(x => x.NameFromProvider == place.Category);
            if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out CategoryTypes foundCategoryEnum))
            {
                placeCategories |= foundCategoryEnum;
            }
            return placeCategories;
        }
    }
}
