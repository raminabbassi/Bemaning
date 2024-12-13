using StaffingSolution.Models;

namespace StaffingSolution.Interfaces
{
    public interface IJobApplicationService
    {
        Task AddJobApplication(JobApplication application);
        Task<List<JobApplication>> GetJobsAppliedByUser(string userEmail);
        Task UpdateJobApplicationStatus(int applicationId, string status);
    }
}