using FluentValidation;
using PaymentMicroservice.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.DestinationAccountId).NotEmpty().WithMessage("destinationAccountId can't be empty");
            RuleFor(p => p.SourceAccountId).NotEmpty().WithMessage("sourceAccountId can't be empty");
            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("Transaction amount must be greater than 0");
        }
    }
}
