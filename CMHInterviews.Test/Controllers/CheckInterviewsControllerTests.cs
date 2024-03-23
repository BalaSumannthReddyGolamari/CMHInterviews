namespace CMHInterviews.Test.Controllers
{
    public class CheckInterviewsControllerTests
    {
        private readonly Mock<IInterviewService> _mockService;
        private readonly Mock<ILogger<CheckInterviewsController>> _mockLogger;
        private readonly CheckInterviewsController _controller;

        public CheckInterviewsControllerTests()
        {
            _mockService = new Mock<IInterviewService>();
            _mockLogger = new Mock<ILogger<CheckInterviewsController>>();
            _controller = new CheckInterviewsController(_mockService.Object, _mockLogger.Object);
        }
        private void ResetMocks()
        {
            _mockService.Reset();
            _mockLogger.Reset();
        }


        [Fact]
        public async Task CheckInterviews_ValidDate_ReturnsOkResult()
        {
            ResetMocks();
            // Arrange
            var date = new InterviewDate { DateOfInterview = DateTime.Now };
            _mockService.Setup(service => service.GetNumberOfInterviews(It.IsAny<DateTime>(), CancellationToken.None))
                .ReturnsAsync(new ScheduledInterviewCount { NumberOfInterviews = 5 });

            // Act
            var result = await _controller.CheckInterviews(date, CancellationToken.None);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<ScheduledInterviewCount>(okResult.Value);
            Assert.Equal(5, model.NumberOfInterviews);
        }

        [Fact]
        public async Task GetNumberOfInterviews_Exception_ReturnsFromRespository()
        {
            ResetMocks();
            // Arrange
            var date = new InterviewDate { DateOfInterview = DateTime.Now };
            _mockService.Setup(service => service.GetNumberOfInterviews(It.IsAny<DateTime?>(), CancellationToken.None))
                .ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CheckInterviews(date, CancellationToken.None);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        [Fact]
        public async Task CheckInterviews_NullDate_ReturnsBadRequest()
        {
            // Arrange
            var date = new InterviewDate { DateOfInterview = null };
            _mockService.Setup(s => s.GetNumberOfInterviews(It.IsAny<DateTime?>(), CancellationToken.None))
                .ThrowsAsync(new ArgumentNullException(nameof(date), "Interview date is null"));

            // Act
            var result = await _controller.CheckInterviews(date, CancellationToken.None);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Interview date is null.", badRequestResult.Value);
        }
    }
}
