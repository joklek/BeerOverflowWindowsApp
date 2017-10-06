using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListFourSquareUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquareInvalidLatidute()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("TEXT", "100", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquareInvalidLongitude()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("100", "TEXT", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void GetBarsFourSquareInvalidRadius()
        {
            // Arrange
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            // Act
            getBars.GetBarsAround("100", "100", "TEXT");
        }
    }
}
