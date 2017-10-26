using BeerOverflowWindowsApp.BarProviders;
using BeerOverflowWindowsApp.Exceptions;
using NUnit.Framework;

namespace BeerOverflowWindowsApp.UnitTests
{
    public class GetBarListGoogleUnitTests
    {
        [Test]
        [TestCase("0.0", "0.0", "1")]
        [TestCase("0.0", "0.0", "0")]
        [TestCase("10", "10", "999")]
        [TestCase("-90.0", "-180.0", "50")]
        [TestCase("90.0", "180.0", "50")]
        [TestCase("90.0", "180.0", "5")]
        [TestCase("-90.0", "180.0", "50")]
        [TestCase("90.0", "-180.0", "50")]
        [TestCase("50.0", "50.0", "50")]
        public void GetBarsGoogle_GetBarsAround_ValidData(string latitude, string longitude, string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            // Assert, should not throw exception
            Assert.NotNull(getBars.GetBarsAround(latitude, longitude, radius));
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidLatidute([Values("T", "", "-91.0", "91.0")] string latitude, 
                                                                [Values("1.0")] string longitude, 
                                                                [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "latitude");
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidLongitude([Values("1.0")] string latitude, 
                                                                 [Values("T", "", "-181.0", "181.0")]string longitude, 
                                                                 [Values("1")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "longitude");
        }

        [Test]
        public void GetBarsGoogle_GetBarsAround_InvalidRadius([Values("1.0")] string latitude, 
                                                              [Values("1.0")] string longitude, 
                                                              [Values("T", "-", "-1", "", "999999")] string radius)
        {
            // Arrange
            var getBars = new GetBarListGoogle();
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == "radius");
        }
    }
}