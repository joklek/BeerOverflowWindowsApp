using System.Collections.Generic;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class ComparerByRatingUnitTests
    {
        [TestMethod]
        public void ComparerByRating_RatingsFirstBigger()
        {
            // arrange  
            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(10);
            barData1.Ratings.Add(20);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(5);
            barData2.Ratings.Add(10);
            var expectedResult = 1;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ComparerByRating_RatingsFirstNotNullSecondNull()
        {
            // arrange  
            var barData1 = new BarData { Ratings = new List<int>() }; ;
            var barData2 = new BarData { Ratings = null }; ;
            var expectedResult = 1;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ComparerByRating_RatingsFirstSmaller()
        {
            // arrange  
            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(5);
            barData1.Ratings.Add(10);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(10);
            barData2.Ratings.Add(20);
            var expectedResult = -1;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ComparerByRating_RatingsFirstNullSecondNotNull()
        {
            // arrange  
            var barData1 = new BarData { Ratings = null };
            var barData2 = new BarData { Ratings = new List<int>() }; ;
            var expectedResult = -1;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ComparerByRating_RatingsEqual()
        {
            // arrange  
            var barData1 = new BarData { Ratings = new List<int>()};
            barData1.Ratings.Add(5);
            barData1.Ratings.Add(10);
            var barData2 = new BarData { Ratings = new List<int>()};
            barData2.Ratings.Add(10);
            barData2.Ratings.Add(5);
            var expectedResult = 0;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void ComparerByRating_RatingsBothNull()
        {
            // arrange  
            var barData1 = new BarData { Ratings = null };
            var barData2 = new BarData { Ratings = null }; ;
            var expectedResult = 0;
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, barData2);

            // assert  
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ComparerByRating_BarsFirstNull()
        {
            // arrange  
            BarData barData2 = new BarData();
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(null, barData2);

            // assert  
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ComparerByRating_BarsSecondNull()
        {
            // arrange  
            BarData barData1 = new BarData();
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(barData1, null);

            // assert  
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ComparerByRating_BarsBothNull()
        {
            // arrange  
            var comparer = new ComparerByRating();

            // act  
            var result = comparer.Compare(null, null);

            // assert  
        }
    }
}
