using PaymentMicroservice.Core.Models;
using System.Collections;
using System.Collections.Generic;

namespace PaymentMicroservice.Data.Repositories
{
    public interface IEntryRepository
    {
        void InsertEntry(Entry entry);
        ICollection<Entry> GetEntries(int accountId);
    }
}
