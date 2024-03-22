namespace CMHInterviews.HttpClients
{
    public interface IHttpClientService
    {
        Task<IEnumerable<TResult>> GetItems<TResult>(string apiUrl, CancellationToken cancellationToken);
    }

}
