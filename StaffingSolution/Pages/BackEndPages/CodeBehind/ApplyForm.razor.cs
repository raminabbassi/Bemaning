using Microsoft.AspNetCore.Components;
using StaffingSolution.Controllers;
using StaffingSolution.Models;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class ApplyForm
    {
        [Parameter]
        public string jobTitle { get; set; }
        private Job job;

        protected override void OnInitialized()
        {
            job = JobController.GetJobs().FirstOrDefault(j => j.Title == jobTitle);

            if (job == null)
            {

                job = new Job
                {
                    Title = "Okänt jobb",
                    Location = "Ingen plats angiven",
                    Description = "Ingen beskrivning tillgänglig",
                    Responsibilities = "Ingen information tillgänglig",
                    Requirements = "Ingen information tillgängliga"
                };
            }
        }
    }
}
