using StaffingSolution.Models;
using Xunit;

namespace StaffingSolution.Tests
{
    public class JobTests
    {
        [Fact]
        public void Job_DefaultConstructor_ShouldSetPropertiesToNull()
        {
            var job = new Job();

            Assert.Null(job.Title);
            Assert.Null(job.Location);
            Assert.Null(job.Description);
            Assert.Null(job.ImageUrl);
            Assert.Null(job.Responsibilities);
            Assert.Null(job.Requirements);
        }

        [Fact]
        public void Job_SetProperties_ShouldStoreValues()
        {
            var job = new Job
            {
                Title = "Software Developer",
                Location = "Stockholm",
                Description = "Develop and maintain software solutions.",
                ImageUrl = "https://example.com/image.jpg",
                Responsibilities = "Packa och organisera varor",
                Requirements = "Minst 1 års erfarenhet av lagerarbete"
            };

            Assert.Equal("Software Developer", job.Title);
            Assert.Equal("Stockholm", job.Location);
            Assert.Equal("Develop and maintain software solutions.", job.Description);
            Assert.Equal("https://example.com/image.jpg", job.ImageUrl);
            Assert.Equal("Packa och organisera varor", job.Responsibilities);
            Assert.Equal("Minst 1 års erfarenhet av lagerarbete", job.Requirements);
        }

        [Theory]
        [InlineData(null, null, null, null, null, null)]
        [InlineData("Tester", "Malmö", "Test description", "http://example.com", "Testing", "2 år erfarenhet")]
        public void Job_SetProperties_ShouldHandleVariousInputs(
            string title, string location, string description, string imageUrl, string responsibilities, string requirements)
        {
            var job = new Job
            {
                Title = title,
                Location = location,
                Description = description,
                ImageUrl = imageUrl,
                Responsibilities = responsibilities,
                Requirements = requirements
            };

            Assert.Equal(title, job.Title);
            Assert.Equal(location, job.Location);
            Assert.Equal(description, job.Description);
            Assert.Equal(imageUrl, job.ImageUrl);
            Assert.Equal(responsibilities, job.Responsibilities);
            Assert.Equal(requirements, job.Requirements);
        }
    }
}
