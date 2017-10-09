using System;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    class BarRating
    {
        private BarDataModel _barsData = null;
        private CompareType _lastCompare = CompareType.None;

        public BarDataModel GetBarsData()
        {
            return _barsData;
        }

        public BarRating()
        {
            _barsData = BarFileReader.ReadData();
        }

        public void AddRating(BarData barData, int rating)
        {
            var barsCount = _barsData.BarsList.Count(x => x.Title == barData.Title);
            if (barsCount > 0)
            {
                var ratings = _barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                _barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> {rating};
                _barsData.BarsList.Add(barData);
            }
            BarFileWriter.SaveData(_barsData);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (_barsData.BarsList.Count(x => x.Title == bar.Title) == 0)
                {
                    _barsData.BarsList.Add( bar );
                }
            }
            BarFileWriter.SaveData(_barsData);
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
            _barsData.BarsList.Sort(comparer);
            if (compareType == _lastCompare)
            {
                _barsData.BarsList.Reverse();
                _lastCompare = CompareType.None;
            }
            else
            {
                _lastCompare = compareType;
            }
        }
    }
}
