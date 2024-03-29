﻿using PaymentMicroservice.Core.Models;
using System;

namespace PaymentMicroservice.Core.ModelVIew
{
    public class PaymentViewPost
    {
        public double Amount { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public DateTime DateTime { get; set; }
        public int NumberOfPortions { get; set; }

        public PaymentViewPost(Payment payment)
        {
            Amount = payment.Amount;
            SourceAccountId = payment.SourceAccountId;
            DestinationAccountId = payment.DestinationAccountId;
            NumberOfPortions = payment.NumberOfInstallments;
        }
    }
}
