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
        public void AddRatingToNullBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            BarData emptyData = null;
            // Act
            rating.AddRating(emptyData, 100);
            // Assert
        }

        [TestMethod]
        public void AddEmptyBarList()
        {
            // Arrange
            BarRating rating = new BarRating();
            List<BarData> emptyData = null;
            // Act
            rating.AddBars(emptyData);
            // Assert
        }
    }
}
