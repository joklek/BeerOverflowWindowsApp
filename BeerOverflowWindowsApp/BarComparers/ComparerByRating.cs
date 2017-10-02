using BeerOverflowWindowsApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                else
                if (barData1.Ratings == null && barData2.Ratings != null)
                {
                    result = 1;
                }
                else
                if (barData1.Ratings != null && barData2.Ratings == null)
                {
                    result = -1;
                }
                else
                {
                    result = string.Compare(barData1.Ratings.Average().ToString(), barData2.Ratings.Average().ToString());
                }
                return result;            
        }
    }
}
