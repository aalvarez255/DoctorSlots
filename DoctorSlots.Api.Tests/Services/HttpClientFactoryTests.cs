using DoctorSlots.Api.Services;
using NUnit.Framework;
using System.Net.Http.Headers;

namespace DoctorSlots.Api.Tests.Services
{
    [TestFixture]
    public class HttpClientFactoryTests
    {
        private HttpClientFactory _factory;

        [SetUp]
        public void HttpClientFactorySetUp()
        {
            _factory = new HttpClientFactory();
        }

        [Test]
        public void CreateNotNullTest()
        {
            //Arrange
            string baseAddress = "http://localhost:55670";
            AuthenticationHeaderValue authHeader = new AuthenticationHeaderValue("Basic", "TEST_TOKEN");

            //Act
            var result = _factory.Create(baseAddress, authHeader);
            
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
