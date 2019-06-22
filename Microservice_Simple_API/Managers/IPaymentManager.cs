using MicroserviceSimpleAPI.Core;
using MicroserviceSimpleAPI.Core.Models;
using System.Threading.Tasks;

namespace MicroserviceSimpleAPI.Managers
{
    interface IAccountManager
    {
        Task<Payment> PostPayment(int sourceAcc, int destinationAcc, double transactionValue, int numberOfPortions);
    }
}
