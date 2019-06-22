using PaymentMicroservice.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Repositories
{
    public interface ICheckingAccountRepository : IDisposable
    {
        Task<CheckingAccount> GetAccountById(int id);
        void Save();
    }
}
