using CMHInterview.Repositories;
using CMHInterview.Services;
using CMHInterviews.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;

namespace CMHInterviews.Test.Services
{
    public class InterviewServiceTests
    {
        private readonly Mock<IInterviewRepository> _mockRepository;
        private readonly Mock<ILogger<InterviewService>> _mockLogger;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly InterviewService _service;

        public InterviewServiceTests()
        {
            _mockRepository = new Mock<IInterviewRepository>();
            _mockLogger = new Mock<ILogger<InterviewService>>();
            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(config => config["ApiSettings:ScheduledNumberOfDays"]).Returns("14");

            _service = new InterviewService(_mockRepository.Object, _mockLogger.Object, _mockConfiguration.Object);
        }

        private void ResetMocks()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
            _mockConfiguration.Reset();
        }

        [Fact]
        public async Task GetNumberOfInterviews_ValidDate_ReturnsScheduledInterviewCount()
        {
            ResetMocks();
            // Arrange
            var date = DateTime.Now;
            var expectedNumberOfInterviews = 1;

            _mockRepository.Setup(repo => repo.GetInterview(CancellationToken.None))
                .ReturnsAsync(GetSampleInterviews());

            // Act
            var result = await _service.GetNumberOfInterviews(date, CancellationToken.None);

            // Assert
            Assert.Equal(expectedNumberOfInterviews, result.NumberOfInterviews);
        }

        [Fact]
        public async Task GetNumberOfInterviews_Exception_ReturnsFromRespository()
        {
            ResetMocks();
            // Arrange
            var date = DateTime.Now;

            _mockRepository.Setup(repo => repo.GetInterview(CancellationToken.None))
                .ThrowsAsync(new Exception());

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _service.GetNumberOfInterviews(date, CancellationToken.None));
        }

        [Fact]
        public async Task GetNumberOfInterviews_NullDate_ThrowsArgumentNullException()
        {
            ResetMocks();
            // Arrange
            var cancellationToken = CancellationToken.None;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.GetNumberOfInterviews(null, cancellationToken));
        }

        [Fact]
        public async Task GetNumberOfInterviews_WhenInterviewsAreNull()
        {
            ResetMocks();
            // Arrange
            var date = DateTime.Now;
            var expectedNumberOfInterviews = 0;

            // Act
            var result = await _service.GetNumberOfInterviews(date, CancellationToken.None);

            // Assert
            Assert.Equal(expectedNumberOfInterviews, result.NumberOfInterviews);
        }

        // Helper method to generate sample interview data
        private List<Interview> GetSampleInterviews()
        {
            return new List<Interview>
            {
                new Interview() {Name = "Joey", DateOfInterview = DateTime.Now}
            };
        }
    }
}
