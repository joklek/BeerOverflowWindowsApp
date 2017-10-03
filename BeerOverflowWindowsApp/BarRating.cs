using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    class BarRating
    {
        private BarDataModel barsData = null;
        public bool SortByTitleDesc { get; set; } = false;
        public bool SortByRatingDesc { get; set; } = false;
        public bool SortByDistance { get; set; } = false;

        public BarDataModel GetBarsData()
        {
            return barsData;
        }

        public BarRating()
        {
            barsData = BarFileReader.ReadData();
        }

        public void AddRating(BarData barData, int rating)
        {
            var barsCount = barsData.BarsList.Where(x => x.Title == barData.Title).Count();
            if (barsCount > 0)
            {
                var ratings = barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings).FirstOrDefault();
                if(ratings == null) { ratings = new List<int> { }; }
                ratings.Add(rating);
                barsData.BarsList.Where(x => x.Title == barData.Title).Select(x => x.Ratings = ratings).ToList();
            }
            else
            {
                barData.Ratings = new List<int> { };
                barData.Ratings.Add(rating);
                barsData.BarsList.Add(barData);
            }
            BarFileWriter.SaveData(barsData);
        }

        public void AddBars(List<BarData> barsList)
        {
            foreach (var bar in barsList)
            {
                if (barsData.BarsList.Where(x => x.Title == bar.Title).Count() == 0)
                {
                    barsData.BarsList.Add( bar );
                }
            }
            BarFileWriter.SaveData(barsData);
        }

        public void Sort(CompareType compareType)
        {
            switch (compareType)
            {
                case CompareType.Title:
                    var titleComparer = new ComparerByTitle();
                    barsData.BarsList.Sort(titleComparer);
                    if (SortByTitleDesc)
                    {
                        barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = !SortByTitleDesc;
                    SortByRatingDesc = false;
                    SortByDistance = false;
                    break;
                case CompareType.Rating:
                    var ratingsComparer = new ComparerByRating();
                    barsData.BarsList.Sort(ratingsComparer);
                    if (SortByRatingDesc)
                    {
                        barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = false;
                    SortByRatingDesc = !SortByRatingDesc;
                    SortByDistance = false;
                    break;
                case CompareType.Distance:
                    var distanceComparer = new ComparerByDistance();
                    barsData.BarsList.Sort(distanceComparer);
                    if (SortByDistance)
                    {
                        barsData.BarsList.Reverse();
                    }
                    SortByTitleDesc = false;
                    SortByRatingDesc = false;
                    SortByDistance = !SortByDistance;
                    break;
            }
        }
    }
}
