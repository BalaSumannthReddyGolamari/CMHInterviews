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

        //[Fact]
        //public async Task CheckInterviews_NullDate_ReturnsBadRequest()
        //{
        //    // Act
        //    ResetMocks();

        //    // Arrange
        //    var expectedErroMessage = "Interview date is null.";
        //    var date = new InterviewDate { DateOfInterview = null };
        //    _mockService.Setup(service => service.GetNumberOfInterviews(It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
        //        .ThrowsAsync(new Exception(expectedErroMessage));

        //    // Act & Assert
        //    await Assert.ThrowsAsync<Exception>(() => _controller.CheckInterviews(date, CancellationToken.None));
        //}

    }
}
