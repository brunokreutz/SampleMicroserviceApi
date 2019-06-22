using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Core.ModelVIew;
using PaymentMicroservice.Data.Validators;
using PaymentMicroservice.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PaymentMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        readonly IPaymentManager _paymentManager;
        public PaymentsController(IPaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }


        // POST api/payment
        [HttpGet("{id}")]
        public async Task<OkObjectResult> Get(int id)
        {
            var result = await _paymentManager.GetPayment(id);
            return Ok(result);
        }
        // POST api/payment
        [HttpPost]
        public async Task<ObjectResult> Post([FromBody] Payment payment)
        {
            PaymentValidator paymaentValidator = new PaymentValidator();
            ValidationResult valitaionResult = paymaentValidator.Validate(payment);
            if (!valitaionResult.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (ValidationFailure failure in valitaionResult.Errors)
                {
                    errors.Add(failure.ErrorMessage);
                }
                return BadRequest(String.Join("\n",errors));
            }

            
            var result = await _paymentManager.PostPayment(new PaymentViewPost(payment));

            if (payment != null)
            {
                return Created("/"+result.Id,result);
            }
            else
            {
                return BadRequest("Insufficient funds");
            }

        }
    }
}
