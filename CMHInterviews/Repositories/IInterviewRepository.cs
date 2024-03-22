namespace CMHInterview.Repositories
{
    public interface IInterviewRepository
    {
        Task<List<Interview>> GetInterview(CancellationToken cancellationToken);
    }
}
