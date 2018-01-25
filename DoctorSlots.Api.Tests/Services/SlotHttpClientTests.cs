using DoctorSlots.Api.Models;
using DoctorSlots.Api.Services;
using DoctorSlots.Api.Services.SlotServiceClient;
using DoctorSlots.Api.Utils;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DoctorSlots.Api.Tests.Services
{
    [TestFixture]
    public class SlotHttpClientTests
    {
        private Mock<IOptions<SlotServiceConfiguration>> _configurationMock;
        private Mock<IEncoder> _encoderMock;
        private Mock<IHttpClientFactory> _httpClientFactoryMock;

        private SlotHttpClient _slotHttpClient;

        [SetUp]
        public void SlotHttpClientSetup()
        {
            _configurationMock = new Mock<IOptions<SlotServiceConfiguration>>();
            _encoderMock = new Mock<IEncoder>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            
            _configurationMock.Setup(x => x.Value).Returns(() => new SlotServiceConfiguration()
            {
                BaseAddress = "http://localhost:55670",
                Password = "test_password",
                Username = "test_username"
            });

            _slotHttpClient = new SlotHttpClient(
                                _configurationMock.Object, 
                                _encoderMock.Object,
                                _httpClientFactoryMock.Object);
        }

        [Test]
        public async Task GetAsyncSuccessTest()
        {
            //Arrange
            StringContent testResponseContent = new StringContent(BaseTests.GetTestJsonString());

            Mock<FakeHttpMessageHandler> httpMessageHandlerMock = new Mock<FakeHttpMessageHandler> { CallBase = true };
            httpMessageHandlerMock.Setup(x => x.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = testResponseContent
            });
            
            _httpClientFactoryMock.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .Returns(() => new HttpClient(httpMessageHandlerMock.Object));

            //Act
            WeeklyAvailability result = await _slotHttpClient.GetAsync<WeeklyAvailability>("http://localhost:55670");
            
            //Assert
            Assert.IsNotNull(result);
        }


        [Test]
        public async Task GetAsyncFailureTest()
        {
            //Arrange
            Mock<FakeHttpMessageHandler> httpMessageHandlerMock = new Mock<FakeHttpMessageHandler> { CallBase = true };
            httpMessageHandlerMock.Setup(x => x.Send(It.IsAny<HttpRequestMessage>())).Returns(new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest
            });
            _httpClientFactoryMock.Setup(x => x.Create(It.IsAny<string>(), It.IsAny<AuthenticationHeaderValue>()))
                .Returns(() => new HttpClient(httpMessageHandlerMock.Object));

            //Act
            WeeklyAvailability result = await _slotHttpClient.GetAsync<WeeklyAvailability>("http://localhost:55670");

            //Assert
            Assert.IsNull(result);
        }
    }
}
