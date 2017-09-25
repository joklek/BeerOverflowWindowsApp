using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class GetBarListGoogleUnitTest
    {
        [TestMethod]
        public void GetBarsGoogleInvalidLatidute()
        {
            GetBarListGoogle getBars = new GetBarListGoogle();
            getBars.GetBarsAround("TEXT", "100", "100");
        }

        [TestMethod]
        public void GetBarsGoogleInvalidLongitude()
        {
            GetBarListGoogle getBars = new GetBarListGoogle();
            getBars.GetBarsAround("100", "TEXT", "100");
        }

        [TestMethod]
        public void GetBarsGoogleInvalidRadius()
        {
            GetBarListGoogle getBars = new GetBarListGoogle();
            getBars.GetBarsAround("100", "100", "TEXT");
        }
    }
}
