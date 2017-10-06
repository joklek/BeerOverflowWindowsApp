using System.Collections.Generic;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class BarComparers_UnitTests
    {
        [TestMethod]
        public void ComparerByRating_Compare_FirstBigger()
        {
            // arrange  

            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(10);
            barData1.Ratings.Add(20);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(5);
            barData2.Ratings.Add(10);
            int expectedResult = -1;
            var comparer = new ComparerByRating();
            // act  

            int result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ComparerByRating_Compare_FirstSmaller()
        {
            // arrange  

            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(5);
            barData1.Ratings.Add(10);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(10);
            barData2.Ratings.Add(20);
            int expectedResult = 1;
            var comparer = new ComparerByRating();
            // act  

            int result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }
        [TestMethod]
        public void ComparerByRating_Compare_Equal()
        {
            // arrange  

            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(5);
            barData1.Ratings.Add(10);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(10);
            barData2.Ratings.Add(5);
            int expectedResult = 0;
            var comparer = new ComparerByRating();
            // act  

            int result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }
    }
}
