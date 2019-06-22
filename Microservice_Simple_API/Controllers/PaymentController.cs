using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;
using MicroserviceSimpleAPI.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice_Simple_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<Payment>> Post([FromBody] int sourceAccount, int destinationAccount, double value, int portion)
        {
            PaymentManager checkingAccountManager = new PaymentManager();
            var payment = await checkingAccountManager.PostPayment(sourceAccount, destinationAccount, value, portion);
            if (payment != null)
            {
                return CreatedAtAction("Payment Post", payment);
            }
            else
            {
                return BadRequest("Insufficient funds");
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
