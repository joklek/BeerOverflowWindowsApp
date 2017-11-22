using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.GoogleDataModel;

namespace BeerOverflowWindowsApp.BarProviders
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


        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = GetBarData(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private IEnumerable<Place> GetBarData(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',').ToList().SelectMany(category => category.Split('|')).Where((c, i) => i % 2 == 0);
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
                var deserialized = FetcherAndDeserializer.FetchAndDeserialize<PlacesApiQueryResponse>(link, _fetcher).Results;
                deserialized.ForEach(x => x.Category = category);
                placeList.AddRange(deserialized);
            }
            return placeList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var placeList = await GetBarDataAsync(latitude, longitude, radius);
            var barList = PlaceListToBarList(placeList);
            return barList;
        }

        private async Task<IEnumerable<Place>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryList = _categoryList.Split(',').ToList().SelectMany(category => category.Split('|')).Where((c, i) => i % 2 == 0);
            var placeList = new List<Place>();
            foreach (var category in categoryList)
            {
                var link = string.Format(_apiLink, latitude, longitude, radius, category, _apiKey);
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
            return new BarData
            {
                Title = place.Name,
                BarId = place.Name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(place),
                Latitude = place.Geometry.Location.Lat,
                Longitude = place.Geometry.Location.Lng,
                Ratings = new List<int>()
            };
        }

        private static CategoryTypes CollectCategories(Place place)
        {
            var placeCategories = CategoryTypes.None;
            var listOfCategories = _categoryList.Split(',').Select
                (category => category.Split('|')).Select
                (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                .ToList();

            var foundCategory = listOfCategories.FirstOrDefault(x => x.NameFromProvider == place.Category);
            CategoryTypes foundCategoryEnum;
            if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out foundCategoryEnum))
            {
                placeCategories |= foundCategoryEnum;
            }
            return placeCategories;
        }
    }
}
