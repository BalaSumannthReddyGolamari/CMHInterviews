namespace CMHInterview.Repositories
{
    public class InterviewRepository : IInterviewRepository
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ILogger<InterviewRepository> _logger;
        private readonly string _apiUrl;

        public InterviewRepository(IHttpClientService httpClientService, ILogger<InterviewRepository> logger, IConfiguration configuration)
        {
            _httpClientService = httpClientService;
            _logger = logger;
            _apiUrl = configuration["ApiSettings:ApiUrl"];
        }

        public async Task<List<Interview>> GetInterview(CancellationToken cancellationToken)
        {
            try
            {
                var interviews = await _httpClientService.GetItems<Interview>(_apiUrl, cancellationToken);
                return interviews.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching interviews.");
                throw;
            }
        }
    }
}
