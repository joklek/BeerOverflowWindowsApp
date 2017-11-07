using System;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Database;

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
            new DatabaseManager().SaveBarRating(barData, rating);
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
