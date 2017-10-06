using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class BarRatingUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddRatingToNullBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            // Act
            rating.AddRating(null, 100);
            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddEmptyBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            // Act
            rating.AddBars(null);
            // Assert
        }
    }
}
