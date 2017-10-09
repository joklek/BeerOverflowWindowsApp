using System;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    class BarRating
    {
        public BarDataModel BarsData { get; set; }
        private CompareType _lastCompare = CompareType.None;

        public BarRating()
        {
            BarsData = BarFileReader.ReadData();
        }

        public void AddRating(BarData barData, int rating)
        {
            var barsCount = BarsData.BarsList.Count(x => x.Title == barData.Title);
            if (barsCount > 0)
            {
                var ratings = BarsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                BarsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> {rating};
                BarsData.BarsList.Add(barData);
            }
            BarFileWriter.SaveData(BarsData);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (BarsData.BarsList.Count(x => x.Title == bar.Title) == 0)
                {
                    BarsData.BarsList.Add( bar );
                }
            }
            BarFileWriter.SaveData(BarsData);
        }

        public void Sort(CompareType compareType)
        {
            switch (compareType)
            {
                case CompareType.Title:
                    SortAndInvertIfNeeded(new ComparerByTitle(), CompareType.Title);
                    break;
                case CompareType.Rating:
                    SortAndInvertIfNeeded(new ComparerByRating(), CompareType.Rating);
                    break;
                case CompareType.Distance:
                    SortAndInvertIfNeeded(new ComparerByDistance(), CompareType.Distance);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }

        private void SortAndInvertIfNeeded(IComparer<BarData> comparer, CompareType compareType)
        {
            BarsData.BarsList.Sort(comparer);
            if (compareType == _lastCompare)
            {
                BarsData.BarsList.Reverse();
                _lastCompare = CompareType.None;
            }
            else
            {
                _lastCompare = compareType;
            }
        }
    }
}
