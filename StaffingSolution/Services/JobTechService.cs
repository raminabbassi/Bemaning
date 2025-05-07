using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using StaffingSolution.Models;
public class JobTechService : IJobTechService
{
    private readonly HttpClient _http;

    public JobTechService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<JobAdDto>> SearchJobsAsync(string query, int limit = 100)
    {
        var url = $"https://jobsearch.api.jobtechdev.se/search?q={query}&sort=pubdate-desc&limit={limit}";
        var response = await _http.GetFromJsonAsync<JobTechResponse>(url);
        return response?.Hits ?? new List<JobAdDto>();
    }

}
public class JobTechResponse
{
    public List<JobAdDto> Hits { get; set; } = new();
}


