using PaymentMicroservice.Core.Enums;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Core.ModelVIew;
using PaymentMicroservice.Data.Repositories;
using PaymentMicroservice.Data.Validators;
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
        public async Task<Payment> PostPayment(PaymentViewPost paymentViewPost)
        {
            Fee fee = _feeRepository.GetFeeByPortion(paymentViewPost.NumberOfPortions);
            CheckingAccount sourceAccount = await _checkingAccountRepository.GetAccountById(paymentViewPost.SourceAccountId);
            CheckingAccount destinationAccount = await _checkingAccountRepository.GetAccountById(paymentViewPost.DestinationAccountId);

            var totalTransactionValue = paymentViewPost.Amount * (1 + fee.Value / 100);
            var portion = totalTransactionValue / fee.NumberOfPortions;
            if (sourceAccount.Balance < portion)
            {
                return null;
            }

            sourceAccount.Balance = sourceAccount.Balance - portion;
            destinationAccount.Balance = destinationAccount.Balance + portion;

            var destinationAccountEntry = new Entry(portion, DateTime.Now, EntryTypeEnum.CREDIT.ToString(), paymentViewPost.DestinationAccountId);
            var sourceAccountEntry = new Entry(portion, DateTime.Now, EntryTypeEnum.DEBIT.ToString(), paymentViewPost.SourceAccountId);

            if (fee.NumberOfPortions > 1)
            {
                for (int i = 1; i < fee.NumberOfPortions; i++)
                {
                    _entryRepository.InsertEntry(new Entry(portion, DateTime.Now.AddMonths(i), EntryTypeEnum.DEBIT.ToString(), paymentViewPost.SourceAccountId));
                    _entryRepository.InsertEntry(new Entry(portion, DateTime.Now.AddMonths(i), EntryTypeEnum.CREDIT.ToString(), paymentViewPost.DestinationAccountId));
                }
            }

            var payment = new Payment(paymentViewPost);

            _entryRepository.InsertEntry(sourceAccountEntry);
            _entryRepository.InsertEntry(destinationAccountEntry);
            _paymentRepository.InsertPayment(payment);
            _checkingAccountRepository.Save();

            return payment;

        }

        public async Task<Payment> GetPayment(int id)
        {
            return await _paymentRepository.GetPaymentById(id);
        }
    }
}
