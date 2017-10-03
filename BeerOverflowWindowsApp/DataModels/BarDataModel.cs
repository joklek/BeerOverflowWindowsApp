using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeerOverflowWindowsApp.DataModels
{
    class BarDataModel
    {
        private double barNameReallySimilarThreshold = 0.52;
        private double barNameLikelySimilarThreshold = 0.15;
        private double barCoordSimilarThreshold = 0.00005;

        public List<BarData> BarsList { get; set; }

        public void CombineLists(List<BarData> secondaryList)
        {
            foreach (var barDatum in secondaryList)
            {
                BarsList.Add(barDatum);
            }

            var lengthPrimary = BarsList.ToArray().Length;

            for (var i = 0; i + 1 < lengthPrimary; i++)
            {
                for (var j = i + 1; j < lengthPrimary; j++)
                {
                    var stringSimilarity = CalculateStringSimilarity(BarsList[i].Title, BarsList[j].Title);
                    if (stringSimilarity >= barNameReallySimilarThreshold ||
                        (stringSimilarity >= barNameLikelySimilarThreshold &&
                        CoordsAreSimilar(BarsList[i].Latitude, BarsList[i].Longitude,
                            BarsList[j].Latitude, BarsList[j].Longitude)))
                    {
                        BarsList.Remove(BarsList[j]);
                        lengthPrimary = BarsList.ToArray().Length;
                    }
                }
            }
        }

        private bool CoordsAreSimilar(double lat1, double lon1, double lat2, double lon2)
        {
            if (CalculateDoubleDiff(lat1, lat2) <= barCoordSimilarThreshold &&
                (CalculateDoubleDiff(lon1, lon2) <= barCoordSimilarThreshold))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double CalculateDoubleDiff(double number1, double number2)
        {
            return Math.Abs(number1 - number2);
        }

        private double CalculateStringSimilarity(string source, string target)
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

        private int ComputeLevenshteinDistance(string source, string target)
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

        private string ToASCII(string s)
        {
            return string.Join("",
                s.Normalize(NormalizationForm.FormD)
                    .Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark));
        }
    }

    public class BarData
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public List<int> Ratings { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}