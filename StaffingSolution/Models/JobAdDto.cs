using System.Text.Json.Serialization;

namespace StaffingSolution.Models
{
    public class JobAdDto
    {
        public string Headline { get; set; }
        public string Workplace { get; set; }
        public string DescriptionText { get; set; }

        [JsonPropertyName("webpage_url")]
        public string WebpageUrl { get; set; }
    }
}
