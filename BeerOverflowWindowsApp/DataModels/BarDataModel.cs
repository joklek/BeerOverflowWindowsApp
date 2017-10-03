using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeerOverflowWindowsApp.DataModels
{
    class BarDataModel
    {
        public List<BarData> BarsList { get; set; }
    }
    public class BarData
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public static List<BarData> CombineLists(List<BarData> primaryList, List<BarData> secondaryList)
        {
            foreach (var barDatum in secondaryList)
            {
                primaryList.Add(barDatum);
            }

            var lengthPrimary = primaryList.ToArray().Length;

            for (var i = 0; i + 1 < lengthPrimary; i++)
            {
                for (var j = i + 1; j < lengthPrimary; j++)
                {
                    var stringSimilarity = CalculateStringSimilarity(primaryList[i].Title, primaryList[j].Title);
                    if (stringSimilarity >= 0.52 ||
                        (stringSimilarity >= 0.15 &&
                        CoordsAreSimilar(primaryList[i].Latitude, primaryList[i].Longitude,
                                         primaryList[j].Latitude, primaryList[j].Longitude)))
                    {
                        primaryList.Remove(primaryList[j]);
                        lengthPrimary = primaryList.ToArray().Length;
                    }
                }
            }
            return primaryList;
        }

        private static bool CoordsAreSimilar(double lat1, double lon1, double lat2, double lon2)
        {
            var threshold = 0.00005;
            if (CalculateDoubleDiff(lat1, lat2) <= threshold &&
                (CalculateDoubleDiff(lon1, lon2) <= threshold))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static double CalculateDoubleDiff(double number1, double number2)
        {
            return Math.Abs(number1 - number2);
        }

        private static double CalculateStringSimilarity(string source, string target)
        {
            if (string.IsNullOrEmpty(source))
                return string.IsNullOrEmpty(target) ? 1 : 0;

            if (string.IsNullOrEmpty(target))
                return string.IsNullOrEmpty(source) ? 1 : 0;

            string asciifiedNormalizedSource = ToASCII(source.ToLower());
            string asciifiedNormalizedTarget = ToASCII(target.ToLower());
            double stepsToSame = ComputeLevenshteinDistance(asciifiedNormalizedSource, asciifiedNormalizedTarget);
            return (1.0 - (stepsToSame / (double)Math.Max(source.Length, target.Length)));
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

            for (int i = 1; i <= sourceLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
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
    }
}