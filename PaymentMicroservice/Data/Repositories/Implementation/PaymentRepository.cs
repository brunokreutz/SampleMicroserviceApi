using Data;
using PaymentMicroservice.Core.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository, IDisposable
    {
        private readonly TransactionContext _context;
        public PaymentRepository(TransactionContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Payment> GetPaymentById(int id)
        {
            return await _context.Payments.FindAsync(id);
        }

        public async void InsertPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
        }

        public Payment GetLastPayment()
        {
            return _context.Payments.OrderByDescending(p => p.Id).FirstOrDefault();
        }
    }
}
