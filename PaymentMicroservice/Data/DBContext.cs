﻿using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Core.Models;

namespace Data
{
    public class TransactionContext : DbContext
    {
        public TransactionContext(DbContextOptions<TransactionContext> options)
           : base(options)
        {
        }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Installment> Entries { get; set; }
    }
}
