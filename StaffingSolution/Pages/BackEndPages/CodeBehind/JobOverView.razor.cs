using StaffingSolution.Models;
using StaffingSolution.Services;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class JobOverView
    {
        private List<JobApplication> jobs;

        protected override async Task OnInitializedAsync()
        {
            var currentUserEmail = AuthService.GetCurrentUserEmail();
            jobs = await JobApplicationService.GetJobsAppliedByUser(currentUserEmail);
        }
    }
}
