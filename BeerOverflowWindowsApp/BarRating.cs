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

        public BarRating()
        {
            BarsData = new BarDataModel();
        }

        public void AddRating(BarData barData, int rating)
        {
            if (barData.Ratings == null)
            {
                barData.Ratings = new List<int>();
            }
            BarsData.Find(x => x.Title == barData.Title).Ratings.Add(rating);
            BarFileWriter.SaveData(barData);
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

        public void Sort(CompareType compareType, bool ascending = true)
        {
            switch (compareType)
            {
                case CompareType.Title:
                    BarsData.Sort(new ComparerByTitle());
                    break;
                case CompareType.Rating:
                    BarsData.Sort(new ComparerByRating());
                    break;
                case CompareType.Distance:
                    BarsData.Sort(new ComparerByDistance());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(compareType), compareType, null);
            }
            if (ascending == false)
            {
                BarsData.Reverse();
            }
        }
    }
}
