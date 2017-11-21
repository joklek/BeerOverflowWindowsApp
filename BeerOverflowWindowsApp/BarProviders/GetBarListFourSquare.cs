using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Utilities;
using static BeerOverflowWindowsApp.DataModels.FourSquareDataModel;

namespace BeerOverflowWindowsApp.BarProviders
{
    class GetBarListFourSquare : IBeerable
    {
        public string ProviderName { get; } = "FourSquare";
        private static readonly string _apiLink = ConfigurationManager.AppSettings["FourSquareAPILink"];
        private static readonly string _clientId = ConfigurationManager.AppSettings["FourSquareClientId"];
        private static readonly string _clientSecret = ConfigurationManager.AppSettings["FourSquareClientSecret"];
        private static readonly string _categoryIdFetced = ConfigurationManager.AppSettings["FourSquareFetchCategoryIDs"];
        private static readonly string _categoryIdBanned = ConfigurationManager.AppSettings["FourSquareBannedCategoryIDs"];
        private static readonly string _categoryIdSecondary = ConfigurationManager.AppSettings["FourSquareSecondaryCategoryIDs"];
        private readonly IHttpFetcher _fetcher;

        public GetBarListFourSquare(IHttpFetcher fetcher)
        {
            _fetcher = fetcher;
        }

        public List<BarData> GetBarsAround(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var venueList = GetBarData(latitude, longitude, radius);
            RemoveBannedVenues(venueList);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private List<Venue> GetBarData (string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdFetced.Split(',').ToList();
            var venueList = new List<Venue>();
            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                venueList.AddRange(FetcherAndDeserializer.FetchAndDeserialize<SearchResponse>(link, _fetcher).response.venues);
            }
            return venueList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(string latitude, string longitude, string radius)
        {
            RegexTools.LocationDataTextIsCorrect(latitude, longitude, radius);
            var venueList = await GetBarDataAsync(latitude, longitude, radius);
            RemoveBannedVenues(venueList);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private void RemoveBannedVenues(List<Venue> venueList)
        {
            var categoryIdBanned = _categoryIdBanned.Split(',').ToList();
            venueList.RemoveAll(x => x.categories.Exists(y => categoryIdBanned.Contains(y.id)));
        }

        private async Task<List<Venue>> GetBarDataAsync(string latitude, string longitude, string radius)
        {
            var categoryIDs = _categoryIdFetced.Split(',').ToList();
            var venueList = new List<Venue>();
            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude, longitude, category, radius);
                var deserialized = await FetcherAndDeserializer.FetchAndDeserializeAsync<SearchResponse>(link, _fetcher);
                venueList.AddRange(deserialized.response.venues);
            }
            return venueList;
        }

        private static List<BarData> VenueListToBarList(IEnumerable<Venue> venueList, string radius)
        {
            var radiusNum = int.Parse(radius, CultureInfo.InvariantCulture);
            return (from venue in venueList where (venue.location.distance <= radiusNum) select VenueToBar(venue)).ToList();
        }

        private static BarData VenueToBar(Venue venue)
        {
            return new BarData
            {
                Title = venue.name,
                BarId = venue.name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(venue),
                Latitude = venue.location.lat,
                Longitude = venue.location.lng,
                Ratings = new List<int>()
            };
        }

        private static CategoryTypes CollectCategories(Venue place)
        {
            var placeCategories = CategoryTypes.None;
            var listOfCategories = _categoryIdSecondary.Split(',').Select
                (category => category.Split('|')).Select
                (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                .ToList();

            foreach (var category in place.categories)
            {
                var foundCategory = listOfCategories.FirstOrDefault(x => x.NameFromProvider == category.id);
                CategoryTypes foundCategoryEnum;
                if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out foundCategoryEnum))
                {
                    placeCategories |= foundCategoryEnum;
                }
            }
            return placeCategories;
        }
    }
}
