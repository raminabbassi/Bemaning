using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using StaffingSolution.Models;
using Xunit;
namespace StaffingSolutionTest
{

    public class OpenAiChatServiceTests
    {
        [Fact]
        public async Task AskAsync_ReturnsExpectedMessage_WhenApiReturnsValidResponse()
        {
            
            var json = @"{
            ""choices"": [
                { ""message"": { ""content"": ""Det här är ett testsvar"" } }
            ]
        }";

            var handler = new MockHttpMessageHandler(json);
            var client = new HttpClient(handler);
            var options = Options.Create(new OpenAISettings { ApiKey = "test-key" });

            var service = new OpenAiChatService(client, options);

            var result = await service.AskAsync("Vad kan jag få hjälp med?");

            Assert.Contains("testsvar", result);
        }

        private class MockHttpMessageHandler : HttpMessageHandler
        {
            private readonly string _response;

            public MockHttpMessageHandler(string response)
            {
                _response = response;
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(_response, Encoding.UTF8, "application/json")
                });
            }
        }
    }

}
