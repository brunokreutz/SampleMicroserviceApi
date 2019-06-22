using PaymentMicroservice.Core.Models;
using System;
using System.Threading.Tasks;

namespace Microservice_Simple_API.Repositories
{
    public interface ICheckingAccountRepository : IDisposable
    {
        Task<CheckingAccount> GetAccountById(int id);
        void Save();
    }
}
