using Data;
using PaymentMicroservice.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PaymentMicroservice.Data.Repositories
{
    public class EntryRepository : IEntryRepository, IDisposable
    {
        private readonly TransactionContext _context;

        public EntryRepository(TransactionContext context)
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

        public ICollection<Entry> GetEntries(int accountId)
        {
            return _context.Entries.Where(e => e.CheckingAccountId == accountId).ToList();
        }
    }
}
