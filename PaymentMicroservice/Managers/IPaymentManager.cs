using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Core.ModelVIew;
using System.Threading.Tasks;

namespace PaymentMicroservice.Managers
{
    public interface IPaymentManager
    {
        Task<Payment> PostPayment(PaymentViewPost paymentViewPost);
        Task<Payment> GetPayment(int id);
    }
}
