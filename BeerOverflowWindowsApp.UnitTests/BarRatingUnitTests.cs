using System;
using BeerOverflowWindowsApp.BarComparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class BarRatingUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BarRating_AddRatingToNullBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            // Act
            rating.AddRating(null, 100);
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void BarRating_AddEmptyBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            // Act
            rating.AddBars(null);
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void BarRating_SortUnknownEnum()
        {
            // Arrange
            BarRating rating = new BarRating();

            // Act
            rating.Sort((CompareType) 99999);
            
            // Assert
        }
    }
}
