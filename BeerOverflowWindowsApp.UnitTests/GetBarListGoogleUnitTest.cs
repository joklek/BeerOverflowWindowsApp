using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeerOverflowWindowsApp.BarProviders;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListGoogleUnitTest
    {
        [TestMethod]
        public void GetBarsGoogle_InvalidLatidute()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("TEXT", "100", "100");
            // Assert, should be no exception
        }

        [TestMethod]
        public void GetBarsGoogle_InvalidLongitude()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("100", "TEXT", "100");
            // Assert, should be no exception
        }

        [TestMethod]
        public void GetBarsGoogle_InvalidRadius()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("100", "100", "TEXT");
            // Assert, should be no exception
        }
    }
}
