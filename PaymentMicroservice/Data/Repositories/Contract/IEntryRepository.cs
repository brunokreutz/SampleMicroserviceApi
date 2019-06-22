using PaymentMicroservice.Core.Models;

namespace PaymentMicroservice.Data.Repositories
{
    public interface IEntryRepository
    {
        void InsertEntry(Entry entry);
    }
}
