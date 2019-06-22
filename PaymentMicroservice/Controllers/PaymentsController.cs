using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Managers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservice_Simple_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        // POST api/payment
        [HttpPost]
        public async Task<ActionResult<Payment>> Post([FromBody] int sourceAccount, int destinationAccount, double value, int numberOfPortions)
        {
            if (numberOfPortions < 1 || numberOfPortions > 3)
            {
                return BadRequest("Number of portions Invalid");
            }

            PaymentManager checkingAccountManager = new PaymentManager();
            var payment = await checkingAccountManager.PostPayment(sourceAccount, destinationAccount, value, numberOfPortions);

            if (payment != null)
            {
                return CreatedAtAction("Payment Post", payment);
            }
            else
            {
                return BadRequest("Insufficient funds");
            }

        }
    }
}
