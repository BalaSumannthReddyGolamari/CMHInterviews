using CMHInterviews.Model;

namespace CMHInterview.Services
{
    public interface IInterviewService
    {
        Task<ScheduledInterviewCount> GetNumberOfInterviews(DateTime? date, CancellationToken cancellationToken);
    }
}
