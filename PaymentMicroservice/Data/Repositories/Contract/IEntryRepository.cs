using PaymentMicroservice.Core.Models;
using System.Collections.Generic;

namespace PaymentMicroservice.Data.Repositories
{
    public interface IEntryRepository
    {
        void InsertEntry(Installment entry);
        ICollection<Installment> GetEntries(int accountId);
    }
}
