using PaymentMicroservice.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Repositories
{
    public interface IPaymentRepository : IDisposable
    {
        Task<Payment> GetPaymentById(int id);
        void InsertPayment(Payment payment);
    }
}
