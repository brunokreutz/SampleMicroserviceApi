using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;
using System;

namespace MicroserviceSimpleAPI.Repositories
{
    interface IFeeRepository : IDisposable
    {
        Fee GetFeeByPortion(int portion);
    }
}
