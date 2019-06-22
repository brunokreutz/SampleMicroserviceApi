using FluentValidation;
using PaymentMicroservice.Core.Models;

namespace PaymentMicroservice.Data.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(p => p.DestinationAccountId).NotEmpty().WithMessage("destinationAccountId can't be empty");
            RuleFor(p => p.SourceAccountId).NotEmpty().WithMessage("sourceAccountId can't be empty");
            RuleFor(p => p.Amount).GreaterThan(0).WithMessage("Transaction amount must be greater than 0");
            RuleFor(p => p.NumberOfPortions).GreaterThan(0).LessThanOrEqualTo(3).WithMessage("Number of portions Invalid");
        }
    }
}
