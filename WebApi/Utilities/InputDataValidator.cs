using System.Collections.Generic;
using WebApi.DataModels;
using WebApi.Exceptions;

namespace WebApi.Utilities
{
    static class InputDataValidator
    {
        private static string _invalidDataMessage = "Invalid data entered";

        public static bool LocationDataIsCorrect(double latitude, double longitude, double radius)
        {
            var latIsCorrect = LatitudeIsCorrect(latitude);
            var lonIsCorrect = LongitudeIsCorrect(longitude);
            var radiusIsCorrect = RadiusIsCorrect(radius);
            var returnValue = latIsCorrect && lonIsCorrect && radiusIsCorrect;

            if (returnValue) return true;
            var badArguments = new List<string>();

            if (!latIsCorrect) { badArguments.Add("latitude"); }
            if (!lonIsCorrect) { badArguments.Add("longitude"); }
            if (!radiusIsCorrect) { badArguments.Add("radius"); }
            var invalidArguments = string.Join(",", badArguments);
            throw new ArgumentsForProvidersException(_invalidDataMessage, invalidArguments);
        }

        public static bool LocationDataIsCorrect(LocationRequestModel location)
        {
            return LocationDataIsCorrect(location.Latitude, location.Longitude, location.Radius);
        }

        public static bool LatitudeIsCorrect(double latitude)
        {
            latitude = latitude < 0 ? -latitude : latitude;
            if (latitude <= 90) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "latitude");
        }

        public static bool LongitudeIsCorrect(double longitude)
        {
            longitude = longitude < 0 ? -longitude : longitude;
            if (longitude <= 180) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "longitude");
        }

        public static bool RadiusIsCorrect(double radius)
        {
            if (radius >= 0 && radius < 1000) return true;
            throw new ArgumentsForProvidersException(_invalidDataMessage, "radius");
        }
    }
}