using Microservice_Simple_API.Core;
using Microservice_Simple_API.Core.Models;
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
