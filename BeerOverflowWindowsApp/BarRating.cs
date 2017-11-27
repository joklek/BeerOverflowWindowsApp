﻿using System;
using System.Collections.Generic;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;

namespace BeerOverflowWindowsApp
{
    public class BarRating
    {
        public List<BarData> BarsData { get; set; }

        public BarRating()
        {
            BarsData = new List<BarData>();
        }

        public void AddRating(BarData barData, int rating)
        {
            if (barData == null)
            {
                throw new ArgumentNullException(nameof(barData), "BarRating.AddRating null parameter");
            }
            WebApiAccess.SaveBarRating(barData, rating);
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
                case CompareType.None:
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
