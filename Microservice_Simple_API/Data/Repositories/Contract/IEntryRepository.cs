using PaymentMicroservice.Core.Models;

namespace PaymentMicroservice.Data.Repositories
{
    interface IEntryRepository
    {
        void InsertEntry(Entry entry);
    }
}
