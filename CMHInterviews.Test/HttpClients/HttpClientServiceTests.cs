using CMHInterviews.HttpClients;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMHInterviews.Test.HttpClients
{
    public class HttpClientServiceTests
    {
        private readonly Mock<HttpClient> _mockHttpClient;
        private readonly Mock<ILogger<HttpClientService>> _mockLogger;
        private readonly HttpClientService _httpClientService;

        public HttpClientServiceTests()
        {
            _mockHttpClient = new Mock<HttpClient>();
            _mockLogger = new Mock<ILogger<HttpClientService>>();
            _httpClientService = new HttpClientService(_mockHttpClient.Object, _mockLogger.Object);
        }

        //[Fact]
        //public async Task GetItems_SuccessfulResponse_ReturnsDeserializedData()
        //{
        //    // Arrange
        //     var apiUrl = "https://example.com/api/getitems";
        //    var expectedResponse = new List<string> { "item1", "item2", "item3" };

        //    // Set up the HTTP client to return a successful response
        //    var handlerMock = new Mock<HttpMessageHandler>();

        //    var serializedResponse = JsonSerializer.Serialize(expectedResponse);
        //    var responseContent = new StringContent(serializedResponse, Encoding.UTF8, "application/json");

        //    var response = new HttpResponseMessage
        //    {
        //        StatusCode = HttpStatusCode.OK,
        //        Content = responseContent
        //    };

        //    handlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
        //        ItExpr.IsAny<CancellationToken>()).ReturnsAsync(response);

        //    var httpClient = new HttpClient(handlerMock.Object)
        //    {
        //        BaseAddress = new Uri(apiUrl)
        //    };

        //    // Act
        //    var result = await _httpClientService.GetItems<string>(apiUrl, CancellationToken.None);

        //    // Assert
        //    Assert.Equal(expectedResponse, result);
        //    _mockLogger.Verify(
        //        l => l.LogInformation($"Fetching interviews from {apiUrl}"),
        //        Times.Once);
        //    _mockLogger.Verify(
        //        l => l.LogInformation("Interviews successfully retrieved."),
        //        Times.Once);
        //}

    }


}
