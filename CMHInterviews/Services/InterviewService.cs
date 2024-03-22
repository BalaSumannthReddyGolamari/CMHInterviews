using CMHInterview.Repositories;
using CMHInterviews.Model;

namespace CMHInterview.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IInterviewRepository _repository;
        private readonly ILogger<InterviewService> _logger;
        private readonly string _scheduledNumberOfDays;

        public InterviewService(IInterviewRepository repository, ILogger<InterviewService> logger, IConfiguration configuration)
        {
            _repository = repository;
            _logger = logger;
            _scheduledNumberOfDays = configuration["ApiSettings:ScheduledNumberOfDays"];
        }

        public async Task<ScheduledInterviewCount> GetNumberOfInterviews(DateTime? date, CancellationToken cancellationToken)
        {
            try
            {
                DateTime endDate = date ?? throw new ArgumentNullException(nameof(date), "Interview date is null.");

                if (string.IsNullOrWhiteSpace(_scheduledNumberOfDays))
                {
                    throw new InvalidOperationException("ScheduledNumberOfDays is not configured.");
                }
                _logger.LogInformation($"Fetching interviews for the next {_scheduledNumberOfDays} days");

                endDate = endDate.AddDays(int.Parse(_scheduledNumberOfDays));

                var interviews = await _repository.GetInterview(cancellationToken);
                if (interviews == null || !interviews?.Any() == null)
                {
                    _logger.LogInformation("No interviews found.");
                    return new ScheduledInterviewCount { NumberOfInterviews = 0 };
                }

                var interviewsForNext14Days = interviews?
                    .Where(interview => interview?.DateOfInterview?.Date >= date?.Date && interview?.DateOfInterview?.Date <= endDate.Date);

                int numberOfInterviews = interviewsForNext14Days?.Count() ?? 0;

                _logger.LogInformation("Number of interviews found: {Count}", numberOfInterviews);

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
