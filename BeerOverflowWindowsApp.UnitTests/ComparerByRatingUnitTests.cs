using System;
using System.Collections.Generic;
using BeerOverflowWindowsApp.BarComparers;
using BeerOverflowWindowsApp.DataModels;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class ComparerByRatingUnitTests
    {
        [Test]
        public void ComparerByRating_Compare_RatingsFirstBigger()
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

        [Test]
        public void ComparerByRating_Compare_RatingsFirstNotNullSecondNull()
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

        [Test]
        public void ComparerByRating_Compare_RatingsFirstSmaller()
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

        [Test]
        public void ComparerByRating_Compare_RatingsFirstNullSecondNotNull()
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

        [Test]
        public void ComparerByRating_Compare_RatingsEqual()
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

        [Test]
        public void ComparerByRating_Compare_RatingsBothNull()
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

        [Test]
        public void ComparerByRating_Compare_BarsFirstNull()
        {
            // arrange  
            var barData2 = new BarData();
            var comparer = new ComparerByRating();

            // act  
            // assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, barData2));
        }

        [Test]
        public void ComparerByRating_Compare_BarsSecondNull()
        {
            // arrange  
            var barData1 = new BarData();
            var comparer = new ComparerByRating();

            // act  
            // assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(barData1, null));
        }

        [Test]
        public void ComparerByRating_Compare_BarsBothNull()
        {
            // arrange  
            var comparer = new ComparerByRating();

            // act  
            // assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, null));
        }
    }
}
