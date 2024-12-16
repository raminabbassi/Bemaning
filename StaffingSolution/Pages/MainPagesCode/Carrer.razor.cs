using StaffingSolution.Controllers;
using StaffingSolution.Models;

namespace StaffingSolution.Pages
{
    public partial class Carrer
    {
        private Job job1;
        private Job job2;
        private Job job3;
        private List<Job> jobs = new List<Job>();
        protected override void OnInitialized()
        {
            jobs = JobController.GetJobs();
            job1 = jobs.FirstOrDefault(j => j.Title == "Webbutvecklare");
            job2 = jobs.FirstOrDefault(j => j.Title == "Projektledare");
            job3 = jobs.FirstOrDefault(j => j.Title == "Marknadsförare");
        }
    }
}
