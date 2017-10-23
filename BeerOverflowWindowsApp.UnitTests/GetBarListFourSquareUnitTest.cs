using System.Net;
using BeerOverflowWindowsApp.BarProviders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListFourSquareUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquare_InvalidLatidute()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("TEXT", "100", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquare_InvalidLongitude()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("100", "TEXT", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquare_InvalidRadius()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("100", "100", "TEXT");
        }
    }
}
