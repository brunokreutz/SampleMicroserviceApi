using Data;
using PaymentMicroservice.Core.Models;
using System;

namespace PaymentMicroservice.Data.Repositories
{
    public class EntryRepository : IEntryRepository, IDisposable
    {
        private readonly DBContext _context;

        public EntryRepository(DBContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async void InsertEntry(Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
        }
    }
}
