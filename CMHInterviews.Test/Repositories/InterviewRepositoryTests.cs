using CMHInterview.Repositories;
using CMHInterview.Services;
using CMHInterviews.Controllers;
using CMHInterviews.HttpClients;
using CMHInterviews.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading;

namespace CMHInterviews.Test.Controllers
{
    public class InterviewRepositoryTests
    {
        private readonly Mock<IHttpClientService> _mockService;
        private readonly Mock<ILogger<InterviewRepository>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly InterviewRepository _repository;

        public InterviewRepositoryTests()
        {
            _mockService = new Mock<IHttpClientService>();
            _mockLogger = new Mock<ILogger<InterviewRepository>>();
            _mockConfiguration = new Mock<IConfiguration>();

            _mockConfiguration.Setup(config => config["ApiSettings:ApiUrl"]).Returns("https://example.com/api/getcandidates");

            _repository = new InterviewRepository(_mockService.Object, _mockLogger.Object, _mockConfiguration.Object);
        }
        private void ResetMocks()
        {
            _mockService.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }

        [Fact]
        public async Task GetInterview_Success_ReturnsInterviews()
        {
            // Arrange
            ResetMocks();
            var expectedInterviews = new List<Interview> { new Interview { Name = "John Doe", DateOfInterview = DateTime.UtcNow } };
            _mockService.Setup(s => s.GetItems<Interview>(It.IsAny<string>(), CancellationToken.None))
                .ReturnsAsync(expectedInterviews);

            // Act
            var result = await _repository.GetInterview(CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedInterviews.Count, result.Count);
        }

        [Fact]
        public async Task GetInterview_ThrowException_ReturnsInterviews()
        {
            // Arrange
            ResetMocks();
            var expectedExceptionMessage = "Error occurred while getting interviews.";
            _mockService.Setup(s => s.GetItems<Interview>(It.IsAny<string>(), CancellationToken.None))
                .ThrowsAsync(new Exception(expectedExceptionMessage));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _repository.GetInterview(CancellationToken.None));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
