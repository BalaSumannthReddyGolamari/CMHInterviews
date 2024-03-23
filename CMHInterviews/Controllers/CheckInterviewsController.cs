namespace CMHInterviews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckInterviewsController : Controller
    {
        private readonly IInterviewService _service;
        private readonly ILogger<CheckInterviewsController> _logger;

        public CheckInterviewsController(IInterviewService service, ILogger<CheckInterviewsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<ScheduledInterviewCount>> CheckInterviews(InterviewDate date, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Received request to check interviews for date: {Date}", date?.DateOfInterview);
                var numberOfInterviews = await _service.GetNumberOfInterviews(date?.DateOfInterview, cancellationToken);
                _logger.LogInformation("Number of interviews found: {Count}", numberOfInterviews);
                return Ok(numberOfInterviews);
            }
            catch (ArgumentNullException ex) when (ex.ParamName == nameof(date))
            {
                _logger.LogError(ex, "Error: Interview date is null.");
                return BadRequest("Interview date is null.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

    }
}
