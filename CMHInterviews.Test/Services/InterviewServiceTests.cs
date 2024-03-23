namespace CMHInterviews.Test.Services
{
    public class InterviewServiceTests
    {
        private readonly Mock<IInterviewRepository> _mockRepository;
        private readonly Mock<ILogger<InterviewService>> _mockLogger;
        private readonly InterviewService _service;

        public InterviewServiceTests()
        {
            _mockRepository = new Mock<IInterviewRepository>();
            _mockLogger = new Mock<ILogger<InterviewService>>();

            _service = new InterviewService(_mockRepository.Object, _mockLogger.Object);
        }

        private void ResetMocks()
        {
            _mockRepository.Reset();
            _mockLogger.Reset();
        }

        [Fact]
        public async Task GetNumberOfInterviews_ValidDate_ReturnsScheduledInterviewCount()
        {
            ResetMocks();
            // Arrange
            var date = DateTime.Now.AddDays(2);
            var expectedNumberOfInterviews = 2;

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
            var currentDate = DateTime.Now.Date;
            return new List<Interview>
                {
                    new Interview() {Name = "Joey", DateOfInterview = currentDate},
                    new Interview() {Name = "Chandler", DateOfInterview = currentDate.AddDays(1)},
                    new Interview() {Name = "Monica", DateOfInterview = currentDate.AddDays(2)},
                    new Interview() {Name = "Rachel", DateOfInterview = currentDate.AddDays(2)},
                    new Interview() {Name = "Ross", DateOfInterview = currentDate.AddDays(3)},
                    new Interview() {Name = "Phoebe", DateOfInterview = currentDate.AddDays(3)},
                    new Interview() {Name = "Janice", DateOfInterview = currentDate.AddDays(3)}
                };
        }

    }
}
