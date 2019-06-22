using Microservice_Simple_API.Core.Models;
using MicroserviceSimpleAPI.Core.Enums;
using MicroserviceSimpleAPI.Core.Models;
using MicroserviceSimpleAPI.Repositories;
using System;
using System.Threading.Tasks;

namespace MicroserviceSimpleAPI.Managers
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
        public async Task<Payment> PostPayment(int sourceAcc, int destinationAcc, double transactionValue, int numberOfPortions)
        {
            CheckingAccount sourceAccount = await CheckingAccountRepository.GetAccountById(sourceAcc);
            CheckingAccount destinationAccount = await CheckingAccountRepository.GetAccountById(destinationAcc);
            Fee fee = FeeRepository.GetFeeByPortion(numberOfPortions);

            var totalTransactionValue = transactionValue * (1 + fee.Value / 100);
            var portion = totalTransactionValue / fee.NumberOfPortions;
            if (sourceAccount.Balance < totalTransactionValue)
            {
                return null;
            }

            sourceAccount.Balance = sourceAccount.Balance - portion;
            destinationAccount.Balance = destinationAccount.Balance + transactionValue;

            var destinationAccountEntry = new Entry(totalTransactionValue, DateTime.Now, EntryTypeEnum.CREDIT.ToString(), destinationAcc);
            EntryRepository.InsertEntry(destinationAccountEntry);
            var sourceAccountEntry = new Entry(portion, DateTime.Now, EntryTypeEnum.DEBIT.ToString(), sourceAcc);
            EntryRepository.InsertEntry(sourceAccountEntry);

            if (fee.NumberOfPortions > 1)
            {
                for (int i = 1; i < fee.NumberOfPortions; i++)
                {
                    EntryRepository.InsertEntry(new Entry(portion, DateTime.Now.AddMonths(i), EntryTypeEnum.DEBIT.ToString(), sourceAcc));
                }
            }

            Payment payment = new Payment();
            payment.SourceAccountId = sourceAccount.Id;
            payment.DestinationAccountId = destinationAccount.Id;
            payment.DateTime = DateTime.Now;
            payment.Amount = transactionValue;

            PaymentRepository.InsertPayment(payment);

            CheckingAccountRepository.Save();

            return payment;

        }
    }
}
