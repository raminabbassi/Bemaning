using StaffingSolution.Models;

public interface IJobTechService
{
    Task<List<JobAdDto>> SearchJobsAsync(string query, int limit = 100);
}