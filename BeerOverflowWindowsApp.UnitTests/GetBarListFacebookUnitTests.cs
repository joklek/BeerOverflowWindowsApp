using System.Globalization;
using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Exceptions;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    public class GetBarListFacebookUnitTests
    {
        [Test]
        [TestCase("0", "0", "1")]
        [TestCase("0", "0", "0")]
        [TestCase("1", "1", "999")]
        [TestCase("-90", "-180", "50")]
        [TestCase("90", "180", "50")]
        [TestCase("90", "180", "5")]
        [TestCase("-90", "180", "50")]
        [TestCase("90", "-180", "50")]
        [TestCase("50", "50", "50")]
        public void GetBarsFacebook_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            // Assert, should not throw exception
            Assert.NotNull(getBars.GetBarsAround(latitude, longitude, radius));
        }

        [Test]
        [TestCase("T", "0", "1")]
        [TestCase("", "0", "1")]
        [TestCase("-91", "1", "50")]
        [TestCase("91", "1", "50")]
        public void GetBarsFacebook_InvalidLatidute(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [Test]
        [TestCase("0", "T", "1")]
        [TestCase("0", "", "1")]
        [TestCase("1", "-181", "50")]
        [TestCase("1", "181", "50")]
        public void GetBarsFacebook_InvalidLongitude(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [Test]
        [TestCase("0", "0", "T")]
        [TestCase("0", "0", "-")]
        [TestCase("0", "0", "-1")]
        [TestCase("0", "0", "")]
        [TestCase("0", "0", "999999")]
        public void GetBarsFacebook_InvalidRadius(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }
    }
}