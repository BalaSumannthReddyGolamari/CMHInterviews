using System.Text.Json.Serialization;

namespace CMHInterviews.Model
{
    public class Interview
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("dateOfInterview")]
        public DateTime? DateOfInterview { get; set; }
    }
}
