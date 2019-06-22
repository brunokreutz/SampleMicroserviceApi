
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
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
            var json = "sourceAccount : 1, destinationAccount : 2, value : 100, numberOfPortions : 2";


            var response = await _client.PostAsync("/api/payments", new StringContent(json, Encoding.UTF8, "application/json"));

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

