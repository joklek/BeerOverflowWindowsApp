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
        public bool SortByTitleDesc { get; set; } = false;
        public bool SortByRatingDesc { get; set; } = false;
        public bool SortByDistance { get; set; } = false;

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
            var barsCount = _barsData.BarsList.Where(x => x.Title == barData.Title).Count();
            if (barsCount > 0)
            {
                var ratings = _barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                _barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> { };
                barData.Ratings.Add(rating);
                _barsData.BarsList.Add(barData);
            }
            BarFileWriter.SaveData(_barsData);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (_barsData.BarsList.Where(x => x.Title == bar.Title).Count() == 0)
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
                    var titleComparer = new ComparerByTitle();
                    _barsData.BarsList.Sort(titleComparer);
                    if (SortByTitleDesc)
                    {
                        _barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = !SortByTitleDesc;
                    SortByRatingDesc = false;
                    SortByDistance = false;
                    break;
                case CompareType.Rating:
                    var ratingsComparer = new ComparerByRating();
                    _barsData.BarsList.Sort(ratingsComparer);
                    if (SortByRatingDesc)
                    {
                        _barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = false;
                    SortByRatingDesc = !SortByRatingDesc;
                    SortByDistance = false;
                    break;
                case CompareType.Distance:
                    var distanceComparer = new ComparerByDistance();
                    _barsData.BarsList.Sort(distanceComparer);
                    if (SortByDistance)
                    {
                        _barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = false;
                    SortByRatingDesc = false;
                    SortByDistance = !SortByDistance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
        }
    }
}
