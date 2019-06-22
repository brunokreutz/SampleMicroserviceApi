using PaymentMicroservice.Core.Enums;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Data.Repositories;
using PaymentMicroservice.Data.Validators;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Managers
{
    public class PaymentManager
    {
        CheckingAccountRepository CheckingAccountRepository;
        PaymentRepository PaymentRepository;
        FeeRepository FeeRepository;
        EntryRepository EntryRepository;
        public PaymentManager()
        {
        }

        public PaymentManager(CheckingAccountRepository accountRepository, PaymentRepository paymentRepository, FeeRepository feeRepository, EntryRepository entryRepository)
        {
            CheckingAccountRepository = accountRepository;
            PaymentRepository = paymentRepository;
            FeeRepository = feeRepository;
            EntryRepository = entryRepository;
        }
        public async Task<Payment> PostPayment(int sourceAccountId, int destinationAccountId, double transactionValue, int numberOfPortions)
        {
            Fee fee = FeeRepository.GetFeeByPortion(numberOfPortions);
            CheckingAccount sourceAccount = await CheckingAccountRepository.GetAccountById(sourceAccountId);
            CheckingAccount destinationAccount = await CheckingAccountRepository.GetAccountById(destinationAccountId);

            var totalTransactionValue = transactionValue * (1 + fee.Value / 100);
            var portion = totalTransactionValue / fee.NumberOfPortions;

            if (sourceAccount.Balance < totalTransactionValue)
            {
                return null;
            }

            Payment payment = new Payment(transactionValue, sourceAccount.Id, destinationAccount.Id, DateTime.Now);
            PaymentValidator paymaentValidator = new PaymentValidator();
            paymaentValidator.Validate(payment);

            sourceAccount.Balance = sourceAccount.Balance - portion;
            destinationAccount.Balance = destinationAccount.Balance + transactionValue;

            var destinationAccountEntry = new Entry(totalTransactionValue, DateTime.Now, EntryTypeEnum.CREDIT.ToString(), destinationAccountId);
            var sourceAccountEntry = new Entry(portion, DateTime.Now, EntryTypeEnum.DEBIT.ToString(), sourceAccountId);
            
            if (fee.NumberOfPortions > 1)
            {
                for (int i = 1; i < fee.NumberOfPortions; i++)
                {
                    EntryRepository.InsertEntry(new Entry(portion, DateTime.Now.AddMonths(i), EntryTypeEnum.DEBIT.ToString(), sourceAccountId));
                }
            }

            EntryRepository.InsertEntry(sourceAccountEntry);
            EntryRepository.InsertEntry(destinationAccountEntry);
            PaymentRepository.InsertPayment(payment);
            CheckingAccountRepository.Save();

            return payment;

        }
    }
}
