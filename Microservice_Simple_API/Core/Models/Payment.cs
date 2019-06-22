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

        public Payment(double amount, int sourceAccountId, int destinationAccountId, DateTime dateTime)
        {
            Amount = amount;
            SourceAccountId = sourceAccountId;
            DestinationAccountId = destinationAccountId;
            DateTime = dateTime;
        }
    }
}
