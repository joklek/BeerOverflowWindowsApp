using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;
using System.Linq;

namespace BeerOverflowWindowsApp.BarComparers
{
    class ComparerByRating : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            int result;
            if (barData1.Ratings == null && barData2.Ratings == null)
            {
                result = 0;
            }
            else if (barData1.Ratings == null && barData2.Ratings != null)
            {
                result = 1;
            }
            else if (barData1.Ratings != null && barData2.Ratings == null)
            {
                result = -1;
            }
            else
            {
                result = barData1.Ratings.Average() ==  barData2.Ratings.Average() 
                    ? 0 
                    : barData1.Ratings.Average() > barData2.Ratings.Average() 
                        ? -1 
                        : 1 ;
            }
            if (result == 0)
            {
                result = string.Compare(barData1.Title, barData2.Title);
            }
            return result;
        }
    }
}
