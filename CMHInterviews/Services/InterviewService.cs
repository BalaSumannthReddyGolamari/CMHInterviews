namespace CMHInterview.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _repository;
        private readonly ILogger<InterviewService> _logger;

        public InterviewService(IInterviewRepository repository, ILogger<InterviewService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<ScheduledInterviewCount> GetNumberOfInterviews(DateTime? date, CancellationToken cancellationToken)
        {
            try
            {
                DateTime endDate = date ?? throw new ArgumentNullException(nameof(date), "Interview date is null.");

                _logger.LogInformation($"Fetching interviews for the next 14 days starting from {date}");

                var interviews = await _repository.GetInterview(cancellationToken);

                if (interviews?.Any() == null)
                {
                    _logger.LogInformation("No interviews found.");
                    return new ScheduledInterviewCount { NumberOfInterviews = 0 };
                }

                //var maxDate = interviews?.Max(i => i.DateOfInterview)?.Date;

                //var minDate = interviews?.Min(i => i.DateOfInterview)?.Date;

                //if (date?.Date < minDate || date?.Date > maxDate)
                //{
                //    return new ScheduledInterviewCount { NumberOfInterviews = 0 };
                //}

                var interviewsForGivenDate = interviews?
                    .Where(interview => interview.DateOfInterview?.Date == date?.Date);

                int numberOfInterviews = interviewsForGivenDate?.Count() ?? 0;

                _logger.LogInformation("Number of interviews found on the given date: {Count}", numberOfInterviews);

                return new ScheduledInterviewCount { NumberOfInterviews = numberOfInterviews };
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Error: Interview date is null.");
                throw ex;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the number of interviews.");
                throw;
            }
        }
    }
}
