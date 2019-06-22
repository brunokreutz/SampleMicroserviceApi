using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace PaymentMicroservice.Test
{
    [TestClass]
    public class PaymentMicroserviceIntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
      private readonly HttpClient _client;

        public PaymentMicroserviceIntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [TestMethod]
        public void TestMethod1()
        {
        }

        [Fact]
        public async Task PostTransaction()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync("/api/payments");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<IEnumerable<Player>>(stringResponse);
            Assert.Contains(players, p => p.FirstName == "Wayne");
            Assert.Contains(players, p => p.FirstName == "Mario");
        }
    }
}
