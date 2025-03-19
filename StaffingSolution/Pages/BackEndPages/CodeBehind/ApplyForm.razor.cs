using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using StaffingSolution.Controllers;
using StaffingSolution.Interfaces;
using StaffingSolution.Models;
using StaffingSolution.Services;

namespace StaffingSolution.Pages.BackEndPages
{
    public partial class ApplyForm
    {
        [Parameter]
        public string jobTitle { get; set; }

        private Job job;
        private JobApplication newApplication = new JobApplication();
        [Inject] private NavigationManager Navigation { get; set; }

        private string firstName = "";
        private string lastName = "";

        [Inject] private IJobApplicationService JobApplicationService { get; set; }
        [Inject] private IAuthService AuthService { get; set; }

        protected override void OnInitialized()
        {
            job = JobController.GetJobs().FirstOrDefault(j => j.Title == jobTitle) ?? new Job
            {
                Title = "Okänt jobb",
                Location = "Ingen plats angiven",
                Description = "Ingen beskrivning tillgänglig",
                Responsibilities = "Ingen information tillgänglig",
                Requirements = "Ingen information tillgänglig"
            };

            newApplication.Title = job.Title;
            newApplication.Company = "Företagsnamn";
            newApplication.UserEmail = AuthService.GetCurrentUserEmail(); 
            newApplication.AppliedDate = DateTime.Now;
        }


        private async Task SubmitApplication()
        {
            JobApplication newApplication = new()
            {
                Title = job.Title,
                Company = "Företagsnamn",
                UserEmail = AuthService.GetCurrentUserEmail(),
                AppliedDate = DateTime.Now
            };

            await JobApplicationService.AddJobApplication(newApplication.Title, newApplication.Company, newApplication.UserEmail);

            Navigation.NavigateTo("/ApplicationPages/Application");
        }




        private async Task HandleFileUpload(InputFileChangeEventArgs e)
        {
            var file = e.File;
            Console.WriteLine($"Uppladdad fil: {file.Name}, Storlek: {file.Size} bytes");
        }
    }
}
