using StaffingSolution.Models;

namespace StaffingSolution.Interfaces
{
    public interface IJobApplicationService
    {
        Task<List<JobApplication>> GetJobsAppliedByUser(string userEmail);
        Task UpdateJobApplicationStatus(int applicationId, string status);
        Task AddJobApplication(string title, string company, string userEmail);   
    }
}