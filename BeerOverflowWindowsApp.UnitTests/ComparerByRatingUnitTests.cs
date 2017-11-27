using System;
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
            // Arrange  
            var barData1 = new BarData { AvgRating = 4.0f };
            var barData2 = new BarData { AvgRating = 3.5f };
            var expectedResult = 1;
            var comparer = new ComparerByRating();

            // Act  
            var result = comparer.Compare(barData1, barData2);

            // Assert  
            Assert.AreEqual(expectedResult, result);
        }    

        [Test]
        public void ComparerByRating_Compare_RatingsFirstSmaller()
        {
            // Arrange  
            var barData1 = new BarData { AvgRating = 2.3f };
            var barData2 = new BarData { AvgRating = 2.5f };
            var expectedResult = -1;
            var comparer = new ComparerByRating();

            // Act  
            var result = comparer.Compare(barData1, barData2);

            // Assert  
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ComparerByRating_Compare_RatingsEqual()
        {
            // Arrange  
            var newAvgRating = 4.3f;
            var barData1 = new BarData { AvgRating = newAvgRating };
            var barData2 = new BarData { AvgRating = newAvgRating };
            var expectedResult = 0;
            var comparer = new ComparerByRating();

            // Act  
            var result = comparer.Compare(barData1, barData2);

            // Assert  
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void ComparerByRating_Compare_BarsFirstNull()
        {
            // Arrange  
            var barData2 = new BarData();
            var comparer = new ComparerByRating();

            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, barData2));
        }

        [Test]
        public void ComparerByRating_Compare_BarsSecondNull()
        {
            // Arrange  
            var barData1 = new BarData();
            var comparer = new ComparerByRating();

            // Act && Assert    
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(barData1, null));
        }

        [Test]
        public void ComparerByRating_Compare_BarsBothNull()
        {
            // Arrange  
            var comparer = new ComparerByRating();

            // Act && Assert  
            Assert.Throws<ArgumentNullException>(() => comparer.Compare(null, null));
        }
    }
}
