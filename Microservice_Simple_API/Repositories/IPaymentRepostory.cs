using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;
using System;
using System.Threading.Tasks;

namespace MicroserviceSimpleAPI.Repositories
{
    interface IPaymentRepostory : IDisposable
    {
        Task<Payment> GetPaymentById(int id);
        void InsertPayment(Payment payment);
    }
}
