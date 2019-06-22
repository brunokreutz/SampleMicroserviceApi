using System;

namespace PaymentMicroservice.Core.Models
{
    public class Entry
    {
        public Entry(double amount, DateTime date, string type, int checkingAccountId)
        {
            Amount = amount;
            Date = date;
            Type = type;
            CheckingAccountId = checkingAccountId;
        }

        public int Id { get; set; }

        public Double Amount { get; set; }
        public DateTime Date { get; set; }
        public String Type { get; set; }
        public int CheckingAccountId { get; set; }
    }
}
