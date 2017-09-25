using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListFourSquareUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void GetBarsFourSquareInvalidLatidute()
        {
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            getBars.GetBarsAround("TEXT", "100", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void GetBarsFourSquareInvalidLongitude()
        {
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            getBars.GetBarsAround("100", "TEXT", "100");
        }

        [TestMethod]
        [ExpectedException(typeof(System.Net.WebException))]
        public void GetBarsFourSquareInvalidRadius()
        {
            GetBarListFourSquare getBars = new GetBarListFourSquare();
            getBars.GetBarsAround("100", "100", "TEXT");
        }
    }
}
