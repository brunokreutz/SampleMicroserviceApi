using Data;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Repositories;
using System;
using System.Linq;

namespace PaymentMicroservice.Data.Repositories
{
    public class FeeRepository : IFeeRepository, IDisposable
    {
        private readonly DBContext _context;

        public FeeRepository(DBContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Fee GetFeeByPortion(int portion)
        {
            return _context.Fees.Where(p => p.NumberOfPortions == portion).FirstOrDefault();
        }
    }
}
