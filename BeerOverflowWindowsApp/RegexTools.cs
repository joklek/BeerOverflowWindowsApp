using System.Text.RegularExpressions;

namespace BeerOverflowWindowsApp
{
    static class RegexTools
    {
        public static bool LocationDataTextIsCorrect(string latitude, string longitude, string radius)
        {
            return LatitudeTextIsCorrect(latitude) && LongitudeTextIsCorrect(longitude) && RadiusTextIsCorrect(radius);
        }

        public static bool LatitudeTextIsCorrect(string latitude)
        {
            return Regex.IsMatch(latitude,
                @"^(-?[1-8]?\d(?:\.\d{1,})?|-?90(?:\.0{1,})?)$");
        }

        public static bool LongitudeTextIsCorrect(string longitude)
        {
            return Regex.IsMatch(longitude,
                @"^(-?(?:1[0-7]|[1-9])?\d(?:\.\d{1,})?|-?180(?:\.0{1,})?)$");
        }

        public static bool RadiusTextIsCorrect(string radius)
        {
            return Regex.IsMatch(radius,
                @"^[0-9]{1,3}$");
        }
    }
}
