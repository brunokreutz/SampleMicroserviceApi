using Data;
using PaymentMicroservice.Core.Models;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Data.Repositories
{
    public class PaymentRepository : IPaymentRepostory, IDisposable
    {
        private readonly DBContext _context;
        public PaymentRepository(DBContext context)
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
    }
}
