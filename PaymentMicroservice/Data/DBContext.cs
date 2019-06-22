using Microsoft.EntityFrameworkCore;
using PaymentMicroservice.Core.Models;

namespace Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
           : base(options)
        {
        }
        public DbSet<CheckingAccount> CheckingAccounts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}
