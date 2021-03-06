﻿using System;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebApi.BarProviders;
using WebApi.Exceptions;
using WebApi.UnitTests.TestData;
using WebApi.Utilities;

namespace WebApi.UnitTests
{
    public class GetBarListGoogleUnitTests
    {
        [TestCaseSource(typeof(BarProviderTestData), "ValidDataCases")]
        public void GetBarsFourSquare_GetBarsAround_ValidData(double latitude, double longitude, double radius)
        {
            // Arrange
            var validFetcher = new Mock<IHttpFetcher>();
            validFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Returns(BarProviderTestData.GoogleValidDataResponse);
            var getBars = new GetBarListGoogle(validFetcher.Object);
            // Act
            var barList = getBars.GetBarsAround(latitude, longitude, radius);
            // Assert
            Assert.NotNull(barList);
            foreach (var bar in barList)
            {
                Assert.NotNull(bar);

                Assert.NotNull(bar.Title);
            }
        }

        [TestCaseSource(typeof(BarProviderTestData), "ValidDataCases")]
        public void GetBarsFourSquare_GetBarsAroundAsync_ValidData(double latitude, double longitude, double radius)
        {
            // Arrange
            var validFetcher = new Mock<IHttpFetcher>();
            validFetcher.Setup(p => p.GetHttpStreamAsync(It.IsAny<string>())).Returns(Task.Run(() => BarProviderTestData.GoogleValidDataResponse));
            var getBars = new GetBarListGoogle(validFetcher.Object);
            // Act
            var barList = getBars.GetBarsAroundAsync(latitude, longitude, radius).Result;
            // Assert
            Assert.NotNull(barList);
            foreach (var bar in barList)
            {
                Assert.NotNull(bar);

                Assert.NotNull(bar.Title);
            }
        }

        [TestCaseSource(typeof(BarProviderTestData), "InvalidDataCases")]
        public void GetBarsGoogle_GetBarsAround_InvalidData(double latitude, double longitude, double radius, string exceptionMessage)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListGoogle(emptyFetcher.Object);
            // Act
            var ex = Assert.Throws<ArgumentsForProvidersException>(() => getBars.GetBarsAround(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == exceptionMessage);
        }

        [TestCaseSource(typeof(BarProviderTestData), "InvalidDataCases")]
        public void GetBarsGoogle_GetBarsAroundAsync_InvalidData(double latitude, double longitude, double radius, string exceptionMessage)
        {
            var emptyFetcher = new Mock<IHttpFetcher>();
            emptyFetcher.Setup(p => p.GetHttpStream(It.IsAny<string>())).Throws(new Exception("This should not have been thrown"));
            // Arrange
            var getBars = new GetBarListGoogle(emptyFetcher.Object);
            // Act
            var ex = Assert.ThrowsAsync<ArgumentsForProvidersException>
                            (async () => await getBars.GetBarsAroundAsync(latitude, longitude, radius));
            // now we can test the exception itself
            Assert.That(ex.InvalidArguments == exceptionMessage);
        }
    }
}
