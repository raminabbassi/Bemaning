using StaffingSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                Responsibilities="Packa och organisera varor",
                Requirements=" Minst 1 års erfarenhet av lagerarbete"
            };

            Assert.Equal("Software Developer", job.Title);
            Assert.Equal("Stockholm", job.Location);
            Assert.Equal("Develop and maintain software solutions.", job.Description);
            Assert.Equal("https://example.com/image.jpg", job.ImageUrl);
            Assert.Equal("Packa och organisera varor", job.Responsibilities);
            Assert.Equal(" Minst 1 års erfarenhet av lagerarbete", job.Requirements);

        }
    }
}
