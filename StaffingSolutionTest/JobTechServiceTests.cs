using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Moq.Protected;
using System.Threading;
using StaffingSolution.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace StaffingSolutionTest
{

    public class JobTechServiceTests
    {
        [Fact]
        public async Task SearchJobsAsync_ReturnsJobList_WhenApiReturnsResults()
        {
            var expectedJobs = new List<JobAdDto> { new JobAdDto { Headline = "Test Job" } };
            var mockResponse = new JobTechResponse { Hits = expectedJobs };
            var mockHttp = new HttpClient(new MockHttpMessageHandler(mockResponse));

            var service = new JobTechService(mockHttp);

            var result = await service.SearchJobsAsync("test");

            Assert.Single(result);
            Assert.Equal("Test Job", result[0].Headline);
        }

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly object _response;

            public MockHttpMessageHandler(object response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                var json = JsonSerializer.Serialize(_response);
                var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
                };
                return Task.FromResult(httpResponse);
            }
        }
    }

}
