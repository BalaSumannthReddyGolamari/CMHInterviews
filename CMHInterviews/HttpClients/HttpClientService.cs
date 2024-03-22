namespace CMHInterviews.HttpClients
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientService> _logger;

        public HttpClientService(HttpClient httpClient, ILogger<HttpClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<TResult>> GetItems<TResult>(string apiUrl, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apiUrl))
                {
                    throw new InvalidOperationException("API URL is not configured.");
                }
                _logger.LogInformation($"Fetching interviews from {apiUrl}");

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Failed to retrieve interviews. Status code: {response.StatusCode}");

                    if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        _logger.LogInformation("No interviews found.");
                        return new List<TResult>();
                    }
                    else
                    {
                        _logger.LogError($"Reason: {response.ReasonPhrase}");
                        throw new HttpRequestException($"Failed to retrieve interviews. Status code: {response.StatusCode}");
                    }
                }
                string responseBody = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrWhiteSpace(responseBody))
                {
                    _logger.LogInformation("Response body is empty. Returning an empty list.");
                    return new List<TResult>(); ;
                }

                _logger.LogInformation("Interviews successfully retrieved.");
                return JsonSerializer.Deserialize<List<TResult>>(responseBody) ?? new List<TResult>();
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching interviews from {apiUrl}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                throw;
            }
        }
    }
}
