using PaymentMicroservice.Core.ModelVIew;
using System;

namespace PaymentMicroservice.Core.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public DateTime DateTime { get; set; }
        public int NumberOfPortions { get; set; }

        public Payment()
        {
        }

        public Payment(PaymentViewPost paymentViewPost)
        {
            Amount = paymentViewPost.Amount;
            SourceAccountId = paymentViewPost.SourceAccountId;
            DestinationAccountId = paymentViewPost.DestinationAccountId;
            DateTime = DateTime.Now;
            NumberOfPortions = paymentViewPost.NumberOfPortions;
        }
    }
}
