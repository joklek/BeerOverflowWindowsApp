using System;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp
{
    public class BarRating
    {
        public BarDataModel BarsData { get; set; }

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
