using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerOverflowWindowsApp.DataModels;
using System.Collections.Generic;

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
