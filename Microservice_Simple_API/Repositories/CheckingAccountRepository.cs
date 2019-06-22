using Microservice_Simple_API.Core;
using Microservice_Simple_API.Core.Models;
using Microservice_Simple_API.Repositories;
using MicroserviceSimpleAPI.Core;
using System;
using System.Threading.Tasks;

namespace MicroserviceSimpleAPI.Repositories
{
    public class CheckingAccountRepository : ICheckingAccountRepository, IDisposable
    {
        private readonly DBContext _context;

        public CheckingAccountRepository(DBContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<CheckingAccount> GetAccountById(int id)
        {
            return await _context.CheckingAccounts.FindAsync(id);
        }

        public async void InsertChecking(CheckingAccount checkingAccount)
        {
            _context.CheckingAccounts.Add(checkingAccount);
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
