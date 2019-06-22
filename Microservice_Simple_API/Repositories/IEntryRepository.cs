using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;

namespace MicroserviceSimpleAPI.Repositories
{
    interface IEntryRepository
    {
        void InsertEntry(Entry entry);
    }
}
