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
        public void GetBarsFacebook_InvalidLatidute([Values("T", "", "-91", "91")] string latitude, 
                                                    [Values("1")] string longitude, 
                                                    [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [Test]
        public void GetBarsFacebook_InvalidLongitude([Values("1")] string latitude, 
                                                     [Values("T", "", "-181", "181")]string longitude, 
                                                     [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListFacebook();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [Test]
        public void GetBarsFacebook_InvalidRadius([Values("1")] string latitude, 
                                                  [Values("1")] string longitude, 
                                                  [Values("T", "-", "-1", "", "999999")] string radius)
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