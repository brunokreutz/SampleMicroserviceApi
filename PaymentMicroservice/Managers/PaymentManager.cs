using PaymentMicroservice.Core.Enums;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Core.ModelVIew;
using PaymentMicroservice.Data.Repositories;
using PaymentMicroservice.Repositories;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Managers
{
    public class PaymentManager : IPaymentManager
    {
        readonly ICheckingAccountRepository _checkingAccountRepository;
        readonly IPaymentRepository _paymentRepository;
        readonly IFeeRepository _feeRepository;
        readonly IEntryRepository _entryRepository;

        public PaymentManager(ICheckingAccountRepository accountRepository, IPaymentRepository paymentRepository, IFeeRepository feeRepository, IEntryRepository entryRepository)
        {
            _checkingAccountRepository = accountRepository;
            _paymentRepository = paymentRepository;
            _feeRepository = feeRepository;
            _entryRepository = entryRepository;
        }
        public async Task<PaymentViewPostOutput> PostPayment(PaymentViewPost paymentViewPost)
        {
            Fee fee = _feeRepository.GetFeeByPortion(paymentViewPost.NumberOfPortions);
            CheckingAccount sourceAccount = await _checkingAccountRepository.GetAccountById(paymentViewPost.SourceAccountId);
            CheckingAccount destinationAccount = await _checkingAccountRepository.GetAccountById(paymentViewPost.DestinationAccountId);

            paymentViewPost.Amount = paymentViewPost.Amount * ((100 + fee.Value) / 100);
            var portion = paymentViewPost.Amount / fee.NumberOfPortions;
            if (sourceAccount.Balance < portion)
            {
                return null;
            }

            sourceAccount.Balance = sourceAccount.Balance - portion;
            destinationAccount.Balance = destinationAccount.Balance + portion;

            var payment = new Payment(paymentViewPost);

            for (int i = 0; i < fee.NumberOfPortions; i++)
            {
                _entryRepository.InsertEntry(new Installment(portion, DateTime.Now.AddMonths(i), InstallmentTypeEnum.DEBIT.ToString(), paymentViewPost.SourceAccountId));
                _entryRepository.InsertEntry(new Installment(portion, DateTime.Now.AddMonths(i), InstallmentTypeEnum.CREDIT.ToString(), paymentViewPost.DestinationAccountId));
            }

            _paymentRepository.InsertPayment(payment);
            _checkingAccountRepository.Save();

            return new PaymentViewPostOutput(paymentViewPost.Amount, sourceAccount.Balance, destinationAccount.Balance);

        }

        public async Task<Payment> GetPayment(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }
    }
}
