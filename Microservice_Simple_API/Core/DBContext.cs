using Microservice_Simple_API.Core;
using Microservice_Simple_API.Core.Models;
using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceSimpleAPI.Core
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
