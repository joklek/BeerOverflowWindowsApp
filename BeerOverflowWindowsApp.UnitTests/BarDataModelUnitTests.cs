using System;
using BeerOverflowWindowsApp.DataModels;
using BeerOverflowWindowsApp.Exceptions;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestFixture]
    public class BarDataModelUnitTests
    {
        [Test]
        [TestCase(null, "90.0", "50")]
        [TestCase("45.0", null, "50")]
        [TestCase("45.0", "90.0", null)]
        [TestCase("45.0", null, null)]
        [TestCase(null, null, "50")]
        [TestCase(null, "90.0", null)]
        [TestCase(null, null, null)]
        public void BarDataModel_CleanUpList_NullStrings(string latitude, string longitude, string radius)
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            // Assert
            var ex = Assert.Throws<ArgumentNullException>(() => barData.CleanUpList(latitude, longitude, radius));
        }

        [Test]
        public void BarDataModel_CleanUpList_InvalidLatidute([Values("T", "", "-91.0", "91.0")] string latitude,
                                                             [Values("1.0")] string longitude,
                                                             [Values("1")] string radius)
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => barData.CleanUpList(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [Test]
        public void BarDataModel_CleanUpList_InvalidLongitude([Values("1.0")] string latitude,
                                                              [Values("T", "", "-181.0", "181.0")]string longitude,
                                                              [Values("1")] string radius)
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => barData.CleanUpList(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [Test]
        public void BarDataModel_CleanUpList_InvalidRadius([Values("1.0")] string latitude,
                                                           [Values("1.0")] string longitude,
                                                           [Values("T", "-", "-1", "", "999999.0")] string radius)
        {
            // Arrange
            var barData = new BarDataModel();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => barData.CleanUpList(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }
    }
}
