using StaffingSolution.Controllers;
using StaffingSolution.Models;
using System.Collections.Generic;
using Xunit;

namespace StaffingSolution.Tests
{
    public class JobControllerTests
    {
        private readonly JobController _controller;

        public JobControllerTests()
        {
            _controller = new JobController();
        }

        [Fact]
        public void GetJobs_ShouldReturnJobList()
        {
            var result = _controller.GetJobs();

            Assert.NotNull(result);
            Assert.IsType<List<Job>>(result);
            Assert.NotEmpty(result);

            var firstJob = result[0];
            Assert.Equal("Lagerarbetare", firstJob.Title);
            Assert.Equal("Vällingby", firstJob.Location);
        }

        [Fact]
        public void GetJobs_ShouldContainSpecificJob()
        {
            var result = _controller.GetJobs();
            var hrJob = result.Find(job => job.Title == "HR-specialist");
            Assert.NotNull(hrJob);
            Assert.Equal("Malmö", hrJob.Location);
            Assert.Contains("rekryteringsprocesser", hrJob.Responsibilities);
        }
    }
}
