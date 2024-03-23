using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.Extensions.Options;
using Moq.Protected;
using System.Net.Http;
using System.Net;
using System.Text.Json;
using Moq;

namespace CMHInterviews.Test.HttpClients
{
    public class HttpClientServiceTests
    {
        private readonly Mock<ILogger<HttpClientService>> _mockLogger;

        public HttpClientServiceTests()
        {
            _mockLogger = new Mock<ILogger<HttpClientService>>();
        }

        private void ResetMocks()
        {
            _mockLogger.Reset();
        }

        [Fact]
        public async Task GetAsync_ShouldPass()
        {
            ResetMocks();
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(GetSampleInterviews())) // mock response
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("http://192.92.36.32");

            var service = new HttpClientService(httpClient, _mockLogger.Object);

            // ACT
            var result = await service.GetItems<Interview>("http:www.google.com", CancellationToken.None);

            // ASSERT
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
            Assert.IsAssignableFrom<List<Interview>>(result);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowException_WhenURLIsNull()
        {
            ResetMocks();
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new Exception("URL is null"))
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("http://192.92.36.32");

            var service = new HttpClientService(httpClient, _mockLogger.Object);

            // ACT & ASSERT
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await service.GetItems<Interview>("", CancellationToken.None));
        }

        [Fact]
        public async Task GetAsync_ShouldPass_WhenNoDaataFound()
        {
            ResetMocks();
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                // prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Content = new StringContent(JsonSerializer.Serialize(new List<Interview>())) // mock response
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("http://192.92.36.32");

            var service = new HttpClientService(httpClient, _mockLogger.Object);

            // ACT
            var result = await service.GetItems<Interview>("http:www.google.com", CancellationToken.None);

            // ASSERT
            handlerMock.Protected().Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>()
            );
            Assert.IsAssignableFrom<List<Interview>>(result);
        }

        [Fact]
        public async Task GetAsync_ShouldThrowHttpException_WhenURLIsNull()
        {
            ResetMocks();
            // ARRANGE
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent(JsonSerializer.Serialize(new List<Interview>())) // mock response
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);
            httpClient.BaseAddress = new Uri("http://192.92.36.32");

            var service = new HttpClientService(httpClient, _mockLogger.Object);

            // ACT & ASSERT
            await Assert.ThrowsAsync<HttpRequestException>(async () => await service.GetItems<Interview>("http:www.google.com", CancellationToken.None));
        }

        private List<Interview> GetSampleInterviews()
        {
            return new List<Interview>
            {
                new Interview() {Name = "Joey", DateOfInterview = DateTime.Now}
            };
        }
    }
}
