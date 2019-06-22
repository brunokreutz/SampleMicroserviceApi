using PaymentMicroservice.Core.Models;
using System.Threading.Tasks;

namespace PaymentMicroservice.Managers
{
    interface IPaymentManager
    {
        Task<Payment> PostPayment(int sourceAcc, int destinationAcc, double transactionValue, int numberOfPortions);
    }
}
