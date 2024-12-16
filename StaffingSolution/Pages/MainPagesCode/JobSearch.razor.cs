using StaffingSolution.Controllers;
using StaffingSolution.Models;

namespace StaffingSolution.Pages
{
    public partial class JobSearch
    {
        private List<Job> jobs = new List<Job>();

        protected override void OnInitialized()
        {
            jobs = JobController.GetJobs();
        }
    }
}
