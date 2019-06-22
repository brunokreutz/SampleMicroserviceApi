using Microservice_Simple_API.Core.Models;
using MicroserviceSimpleAPI.Core;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Microservice_Simple_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<DBContext>();

                LoadInitialData(services);
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
        private static void LoadInitialData(IServiceProvider serviceProvider)
        {
            using (var context = new DBContext(serviceProvider.GetRequiredService<DbContextOptions<DBContext>>()))
            {
                // Look for any board games.
                if (context.Fees.Any())
                {
                    return;   // Data was already seeded
                }

                //context.Fees.AddRange(
                //    new Fee
                //    {
                //        Id = 0,
                //        NumberOfPortions = 1,
                //        Value = 3.79

                //    },
                //    new Fee
                //    {
                //        Id = 1,
                //        NumberOfPortions = 2,
                //        Value = 5.78

                //    },
                //    new Fee
                //    {
                //        Id = 2,
                //        NumberOfPortions = 3,
                //        Value = 7.77

                //    });

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
