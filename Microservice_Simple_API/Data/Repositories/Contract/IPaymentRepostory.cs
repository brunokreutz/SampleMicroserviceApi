using PaymentMicroservice.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Repositories
{
    interface IPaymentRepostory : IDisposable
    {
        Task<Payment> GetPaymentById(int id);
        void InsertPayment(Payment payment);
    }
}
