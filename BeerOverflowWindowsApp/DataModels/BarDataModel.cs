using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BeerOverflowWindowsApp.DataModels
{
   public class BarDataModel
    {
        private readonly double _barNameSimilarThreshold = 
            double.Parse(ConfigurationManager.AppSettings["barNameSimilarThreshold"], CultureInfo.InvariantCulture);
        private readonly double _barNameLikelySimilarThreshold = 
            double.Parse(ConfigurationManager.AppSettings["barNameLikelySimilarThreshold"], CultureInfo.InvariantCulture);
        private readonly double _barCoordSimilarThreshold = 
            double.Parse(ConfigurationManager.AppSettings["barCoordSimilarThreshold"], CultureInfo.InvariantCulture);

        public List<BarData> BarsList { get; set; }

        public void CombineLists(List<BarData> secondaryList)
        {
            BarsList.AddRange(secondaryList);
            RemoveDuplicates();
        }

        private void RemoveDuplicates ()
        {
            var length = BarsList.Count;

            for (var i = 0; i + 1 < length; i++)
            {
                for (var j = i + 1; j < length; j++)
                {
                    var stringSimilarity = CalculateStringSimilarity(BarsList[i].Title, BarsList[j].Title);
                    var coordCheck = CoordsAreSimilar(BarsList[i].Latitude, BarsList[i].Longitude,
                        BarsList[j].Latitude, BarsList[j].Longitude);
                    if (stringSimilarity >= _barNameSimilarThreshold ||
                        (stringSimilarity >= _barNameLikelySimilarThreshold && coordCheck))
                    {
                        // Prints out merged bars, their string similarity score and the difference of coordinates
                        // Commented for testing purposes, because this process needs to be improved
                        /*Console.Write(BarsList[i].Title + " = " + BarsList[j].Title + " :" + stringSimilarity + " " + coordCheck + "\n" +
                                        CalculateDoubleDiff(BarsList[i].Latitude, BarsList[j].Latitude).ToString("N6") + " " +
                                        CalculateDoubleDiff(BarsList[i].Longitude, BarsList[j].Longitude).ToString("N6") + "\n\n");*/
                        BarsList.Remove(BarsList[j]);
                        length--;
                    }
                }
            }
        }

        private bool CoordsAreSimilar(double lat1, double lon1, double lat2, double lon2)
        {
            return CalculateDoubleDiff(lat1, lat2) <= _barCoordSimilarThreshold &&
                   (CalculateDoubleDiff(lon1, lon2) <= _barCoordSimilarThreshold);
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

            var asciifiedNormalizedSource = ToASCII(source.ToLower());
            var asciifiedNormalizedTarget = ToASCII(target.ToLower());
            double stepsToSame = ComputeLevenshteinDistance(asciifiedNormalizedSource, asciifiedNormalizedTarget);
            return (1.0 - (stepsToSame / Math.Max(source.Length, target.Length)));
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