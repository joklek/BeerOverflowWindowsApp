using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace BeerOverflowWindowsApp.UnitTests
{
    [TestClass]
    public class BarFileReaderUnitTest
    {
        private const string filePath = @".\barsData.txt";
        [TestMethod]
        public void ReadNonExistentFile()
        {
            // Arrange
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // Act
            new BarFileReader().ReadData();
            // No exception should be thrown
        }
    }
}
