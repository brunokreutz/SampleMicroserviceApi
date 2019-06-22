using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PaymentMicroservice.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice
{
    public class SeedData
    {
        public static void PopulateDatabase(IServiceProvider serviceProvider)
        {
            using (var context = new TransactionContext(serviceProvider.GetRequiredService<DbContextOptions<TransactionContext>>()))
            {
                // Look for any board games.
                if (context.Fees.Any())
                {
                    return;   // Data was already seeded
                }

                context.Fees.AddRange(
                    new Fee
                    {
                        Id = 1,
                        NumberOfPortions = 1,
                        Value = 3.79

                    },
                    new Fee
                    {
                        Id = 2,
                        NumberOfPortions = 2,
                        Value = 5.78

                    },
                    new Fee
                    {
                        Id = 3,
                        NumberOfPortions = 3,
                        Value = 7.77

                    });

                context.CheckingAccounts.AddRange(
                    new CheckingAccount
                    {
                        Id = 1,
                        Balance = 1000000

                    },
                    new CheckingAccount
                    {
                        Id = 2,
                        Balance = 1000000

                    });
                context.SaveChanges();
            }
        }
    }
}
