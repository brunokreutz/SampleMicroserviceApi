
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PaymentMicroservice.Core.Models;
using System;
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
        private readonly WebApplicationFactory<Startup> _factory;

        //public PaymentMicroserviceIntegrationTest(WebApplicationFactory<Startup> factory)
        //{
        //    _factory = factory;
        //}

        public PaymentMicroserviceIntegrationTest() { }

        [TestMethod]
        public async Task PostTransaction()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            var _client = server.CreateClient();
            // Arrange 
            

            var json = "{\"Amount\":100.0,\"SourceAccountId\":1,\"DestinationAccountId\":2,\"NumberOfPortions\":2}";
            var str = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/payments", str);

            //Act


            // Assert
            Console.WriteLine(response.Content);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        //[Theory]
        //[InlineData("/")]
        //[InlineData("/Index")]
        //[InlineData("/Api/ ")]
        //[InlineData("/Privacy")]
        //[InlineData("/Contact")]
        //public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    // Act
        //    var response = await client.GetAsync(url);

        //    // Assert
        //    response.EnsureSuccessStatusCode(); // Status Code 200-299
        //    Assert.Equals("text/html; charset=utf-8",
        //        response.Content.Headers.ContentType.ToString());
        //}
    }
}

