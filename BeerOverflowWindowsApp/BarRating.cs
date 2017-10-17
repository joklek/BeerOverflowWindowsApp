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
            BarsData = new BarDataModel();
        }

        public void AddRating(BarData barData, int rating)
        {
            var allBars = BarFileReader.GetAllBarData();

            if (barData.Ratings == null)
            {
                barData.Ratings = new List<int>();
            }
            barData.Ratings.Add(rating);
            // Update local copy of list
            BarsData.Find(x => x == barData).Ratings = barData.Ratings;

            var foundBar = allBars.FindIndex(x => x.Title == barData.Title);
            if (foundBar != -1)
            {
                allBars[foundBar].Ratings = barData.Ratings;
            }
            else
            {
                allBars.Add(barData);
            }

            BarFileWriter.SaveData(allBars);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (BarsData.Count(x => x.Title == bar.Title) == 0)
                {
                    BarsData.Add( bar );
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
                case CompareType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }

        private void SortAndInvertIfNeeded(IComparer<BarData> comparer, CompareType compareType)
        { 
            if (BarsData != null)
            {
                BarsData.Sort(comparer);
                if (compareType == _lastCompare)
                {
                    BarsData.Reverse();
                    _lastCompare = CompareType.None;
                }
                else
                {
                    _lastCompare = compareType;
                }
            }
        }

        public void ResetLastCompare()
        {
            _lastCompare = CompareType.None;
        }
    }
}
