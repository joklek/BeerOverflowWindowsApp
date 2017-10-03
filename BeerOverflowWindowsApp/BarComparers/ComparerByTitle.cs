using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;

namespace BeerOverflowWindowsApp.BarComparers
{
    public class ComparerByTitle : IComparer<BarData>
    {
        public int Compare(BarData barData1, BarData barData2)
        {
            return string.Compare(barData1.Title, barData2.Title);           
        }
    }
}
