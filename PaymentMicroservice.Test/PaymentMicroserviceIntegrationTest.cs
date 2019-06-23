using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace PaymentMicroservice.Test
{
    [TestClass]
    public class PaymentMicroserviceIntegrationTest
    {
        HttpClient _client;
        public PaymentMicroserviceIntegrationTest()
        {
            var server = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>());
            SeedData.PopulateDatabase(server.Host.Services);
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task PostTransaction()
        {
            var json = "{\"Amount\":100.0,\"SourceAccountId\":1,\"DestinationAccountId\":2,\"NumberOfPortions\":2}";
            var str = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/payments", str);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public async Task PostInvalidTransaction()
        {
            var json = "{\"Amount\":100.0,\"SourceAccountId\":1,\"DestinationAccountId\":2,\"NumberOfPortions\":4}";
            var str = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/payments", str);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task PostInvalidTransaction2()
        {
            var json = "{\"Amount\":-100.0,\"SourceAccountId\":1,\"DestinationAccountId\":2,\"NumberOfPortions\":2}";
            var str = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/payments", str);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [TestMethod]
        public async Task PostInvalidTransaction3()
        {
            var json = "{\"Amount\":100.0,\"SourceAccountId\":1,\"DestinationAccountId\":2,\"NumberOfPortions\":0}";
            var str = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/payments", str);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}

