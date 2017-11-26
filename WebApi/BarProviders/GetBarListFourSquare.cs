using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Utilities;
using BarData = WebApi.DataModels.BarData;
using CategoryTypes = WebApi.DataModels.CategoryTypes;
using CategoryUnconverted = WebApi.DataModels.CategoryUnconverted;
using FetcherAndDeserializer = WebApi.Utilities.FetcherAndDeserializer;
using FourSquareDataModel = WebApi.DataModels.FourSquareDataModel;
using IHttpFetcher = WebApi.Utilities.IHttpFetcher;

namespace WebApi.BarProviders
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

        public List<BarData> GetBarsAround(double latitude, double longitude, double radius)
        {
            InputDataValidator.LocationDataIsCorrect(latitude, longitude, radius);
            var venueList = GetBarData(latitude, longitude, radius);
            RemoveBannedVenues(venueList);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private List<FourSquareDataModel.Venue> GetBarData (double latitude, double longitude, double radius)
        {
            var categoryIDs = _categoryIdFetced.Split(',').ToList();
            var venueList = new List<FourSquareDataModel.Venue>();
            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), category, radius.ToString(CultureInfo.InvariantCulture));
                venueList.AddRange(FetcherAndDeserializer.FetchAndDeserialize<FourSquareDataModel.SearchResponse>(link, _fetcher).response.venues);
            }
            return venueList;
        }

        public async Task<List<BarData>> GetBarsAroundAsync(double latitude, double longitude, double radius)
        {
            InputDataValidator.LocationDataIsCorrect(latitude, longitude, radius);
            var venueList = await GetBarDataAsync(latitude, longitude, radius);
            RemoveBannedVenues(venueList);
            var barList = VenueListToBarList(venueList, radius);
            return barList;
        }

        private void RemoveBannedVenues(List<FourSquareDataModel.Venue> venueList)
        {
            var categoryIdBanned = _categoryIdBanned.Split(',').ToList();
            venueList.RemoveAll(x => x.categories.Exists(y => categoryIdBanned.Contains(y.id)));
        }

        private async Task<List<FourSquareDataModel.Venue>> GetBarDataAsync(double latitude, double longitude, double radius)
        {
            var categoryIDs = _categoryIdFetced.Split(',').ToList();
            var venueList = new List<FourSquareDataModel.Venue>();
            foreach (var category in categoryIDs)
            {
                var link = string.Format(_apiLink, _clientId, _clientSecret, latitude.ToString(CultureInfo.InvariantCulture), longitude.ToString(CultureInfo.InvariantCulture), category, radius.ToString(CultureInfo.InvariantCulture));
                var deserialized = await FetcherAndDeserializer.FetchAndDeserializeAsync<FourSquareDataModel.SearchResponse>(link, _fetcher);
                venueList.AddRange(deserialized.response.venues);
            }
            return venueList;
        }

        private static List<BarData> VenueListToBarList(IEnumerable<FourSquareDataModel.Venue> venueList, double radius)
        {
            return (from venue in venueList where (venue.location.distance <= radius) select VenueToBar(venue)).ToList();
        }

        private static BarData VenueToBar(FourSquareDataModel.Venue venue)
        {
            return new BarData
            {
                Title = venue.name,
                BarId = venue.name,   // Temporary solution until we decide on BarId 
                Categories = CollectCategories(venue),
                Latitude = venue.location.lat,
                Longitude = venue.location.lng,
            };
        }

        private static CategoryTypes CollectCategories(FourSquareDataModel.Venue place)
        {
            var placeCategories = CategoryTypes.None;
            var listOfCategories = _categoryIdSecondary.Split(',').Select
                (category => category.Split('|')).Select
                (splitCategory => new CategoryUnconverted { NameFromProvider = splitCategory[0], NameNormalized = splitCategory[1] })
                .ToList();

            foreach (var category in place.categories)
            {
                var foundCategory = listOfCategories.FirstOrDefault(x => x.NameFromProvider == category.id);
                if (foundCategory != null && Enum.TryParse(foundCategory.NameNormalized, true, out CategoryTypes foundCategoryEnum))
                {
                    placeCategories |= foundCategoryEnum;
                }
            }
            return placeCategories;
        }
    }
}
