using System;
using System.Collections.Generic;
using System.Configuration;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Text;
using WebApi.Utilities;

namespace WebApi.DataModels
{
    public class BarDataModel : List<BarData>
    {
        private readonly double _barNameSimilarThreshold = double.Parse(ConfigurationManager.AppSettings["barNameSimilarThreshold"], CultureInfo.InvariantCulture);
        private readonly double _barNameLikelySimilarThreshold = double.Parse(ConfigurationManager.AppSettings["barNameLikelySimilarThreshold"], CultureInfo.InvariantCulture);
        private readonly double _barNearnessThresholdInMeters = double.Parse(ConfigurationManager.AppSettings["barNearnessInMetersThreshold"], CultureInfo.InvariantCulture);
        private readonly int _maxSameBarDistanceErrorThresholdMeters = int.Parse(ConfigurationManager.AppSettings["maxSameBarDistanceErrorThresholdMeters"], CultureInfo.InvariantCulture);

        public void RemoveDuplicates()
        {
            for (var i = 0; i + 1 < this.Count; i++)
            {
                for (var j = i + 1; j < this.Count; j++)
                {
                    var stringSimilarity = CalculateStringSimilarity(this[i].Title, this[j].Title);
                    var distanceBetweenBars = GetDistanceBetweenBars(this[i], this[j]);
                    var coordCheck = distanceBetweenBars <= _barNearnessThresholdInMeters;
                    if (OneNameContainsTheOther(this[i].Title, this[j].Title) ||
                        distanceBetweenBars <= _maxSameBarDistanceErrorThresholdMeters &&
                        stringSimilarity >= _barNameSimilarThreshold ||
                        (stringSimilarity >= _barNameLikelySimilarThreshold && coordCheck))
                    {
                        if (this[i].City == null && this[j].City != null ||
                            this[i].City != null && this[j].City != null && 
                            this[i].City.Length < this[j].City.Length)
                        {
                            this[i].City = this[j].City;
                        }
                        if (this[i].StreetAddress == null && this[j].StreetAddress != null ||
                            this[i].StreetAddress != null && this[j].StreetAddress != null &&
                            !this[i].StreetAddress.Any(char.IsDigit) && this[j].StreetAddress.Any(char.IsDigit))
                        {
                            this[i].StreetAddress = this[j].StreetAddress;
                        }
                        // Prints out merged bars, their string similarity score, distance between and either one of them had the other in their name
                        // Commented for testing purposes, because this process needs to be improved
                        /*Console.Write("REMOVED: " + this[i].Title + " = " + this[j].Title + " :" + stringSimilarity + " : " +
                                      GetDistanceBetweenBars(this[i].Latitude, this[i].Longitude,
                                                             this[j].Latitude, this[j].Longitude).ToString("N3") + " meters away" +
                                                             "One bar had other in name?:" + OneNameContainsTheOther(this[i].Title, this[j].Title) + "\n\n");*/
                        this[i].Categories |= this[j].Categories;
                        this.Remove(this[j]);
                        j--;
                    }
                }
            }
        }

        private double GetDistanceBetweenBars(BarData bar1, BarData bar2)
        {
            var coord1 = new GeoCoordinate(bar1.Latitude, bar1.Longitude);
            var coord2 = new GeoCoordinate(bar2.Latitude, bar2.Longitude);
            var distance = coord1.GetDistanceTo(coord2);
            return distance;
        }

        private static double CalculateStringSimilarity(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 1 : 0;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 1 : 0;

            var asciifiedNormalizedSource = ToASCII(source.ToLower());
            var asciifiedNormalizedTarget = ToASCII(target.ToLower());

            double stepsToSame = ComputeLevenshteinDistance(asciifiedNormalizedSource, asciifiedNormalizedTarget);
            return 1.0 - stepsToSame / Math.Max(source.Length, target.Length);
        }

        private static bool OneNameContainsTheOther(string name1, string name2)
        {
            var asciifiedNormalizedName1 = ToASCII(name1.ToLower());
            asciifiedNormalizedName1 = new string(asciifiedNormalizedName1.Where(char.IsLetterOrDigit).ToArray());
            var asciifiedNormalizedName2 = ToASCII(name2.ToLower());
            asciifiedNormalizedName2 = new string(asciifiedNormalizedName2.Where(char.IsLetterOrDigit).ToArray());
            return asciifiedNormalizedName1.Contains(asciifiedNormalizedName2) ||
                   asciifiedNormalizedName2.Contains(asciifiedNormalizedName1);
        }

        private static int ComputeLevenshteinDistance(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 0 : target.Length;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 0 : source.Length;

            var sourceLength = source.Length;
            var targetLength = target.Length;

            var distance = new int[sourceLength + 1, targetLength + 1];

            // Step 1
            for (var i = 0; i <= sourceLength; distance[i, 0] = i++) ;
            for (var j = 0; j <= targetLength; distance[0, j] = j++) ;

            for (var i = 1; i <= sourceLength; i++)
            {
                for (var j = 1; j <= targetLength; j++)
                {
                    // Step 2
                    var cost = (target[j - 1] == source[i - 1]) ? 0 : 1;

                    // Step 3
                    distance[i, j] = Math.Min(
                        Math.Min(distance[i - 1, j] + 1, distance[i, j - 1] + 1),
                        distance[i - 1, j - 1] + cost);
                }
            }
            return distance[sourceLength, targetLength];
        }

        private static string ToASCII(string s)
        {
            return string.Join("",
                s.Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }

        public void RemoveBarsOutsideRadius(double radius)
        {
            InputDataValidator.RadiusIsCorrect(radius);
            var barsToRemove = new List<BarData>(_maxSameBarDistanceErrorThresholdMeters);
            barsToRemove.AddRange(this.Where(bar => bar.DistanceToCurrentLocation > radius));
            foreach (var bar in barsToRemove)
            {
                this.Remove(bar);
            }
        }
    }
}
