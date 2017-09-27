using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListGoogleUnitTest
    {
        [TestMethod]
        public void GetBarsGoogleInvalidLatidute()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("TEXT", "100", "100");
            // Assert, should be no exception
        }

        [TestMethod]
        public void GetBarsGoogleInvalidLongitude()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("100", "TEXT", "100");
            // Assert, should be no exception
        }

        [TestMethod]
        public void GetBarsGoogleInvalidRadius()
        {
            // Arrange
            GetBarListGoogle getBars = new GetBarListGoogle();
            // Act
            getBars.GetBarsAround("100", "100", "TEXT");
            // Assert, should be no exception
        }
    }
}
