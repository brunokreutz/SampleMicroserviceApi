using PaymentMicroservice.Core.Models;
using System;

namespace PaymentMicroservice.Repositories
{
    interface IFeeRepository : IDisposable
    {
        Fee GetFeeByPortion(int portion);
    }
}
