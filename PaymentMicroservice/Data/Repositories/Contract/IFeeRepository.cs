using PaymentMicroservice.Core.Models;
using System;

namespace PaymentMicroservice.Repositories
{
    public interface IFeeRepository : IDisposable
    {
        Fee GetFeeByPortion(int portion);
    }
}
