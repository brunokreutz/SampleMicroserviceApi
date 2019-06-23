using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentMicroservice.Core.Enums;
using PaymentMicroservice.Core.Models;
using PaymentMicroservice.Core.ModelVIew;
using PaymentMicroservice.Data.Repositories;
using PaymentMicroservice.Managers;
using PaymentMicroservice.Repositories;
using System;
using System.Threading.Tasks;

namespace PaymentMicroservice.Test
{
    [TestClass]
    public class PaymentManagerAndRepositoriesTest
    {
        ICheckingAccountRepository _checkingAccountRepository;
        IPaymentRepository _paymentRepository;
        IFeeRepository _feeRepository;
        IEntryRepository _entryRepository;
        public PaymentManagerAndRepositoriesTest()
        {
            var server = new TestServer(new WebHostBuilder()
                   .UseStartup<Startup>());
            SeedData.PopulateDatabase(server.Host.Services);

            _checkingAccountRepository = (ICheckingAccountRepository)server.Host.Services.GetService(typeof(ICheckingAccountRepository));
            _paymentRepository = (IPaymentRepository)server.Host.Services.GetService(typeof(IPaymentRepository));
            _feeRepository = (IFeeRepository)server.Host.Services.GetService(typeof(IFeeRepository));
            _entryRepository = (IEntryRepository)server.Host.Services.GetService(typeof(IEntryRepository));

        }

        [TestMethod]
        public async Task PostPaymentEntries()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 3;

            var result = await manager.PostPayment(new PaymentViewPost(payment));

            var sourceEntries = _entryRepository.GetEntries(payment.SourceAccountId);
            var destinationEntries = _entryRepository.GetEntries(payment.DestinationAccountId);

            Assert.AreEqual(sourceEntries.Count, 3);
            Assert.AreEqual(destinationEntries.Count, 3);

            var i = 0;
            foreach (Installment entry in sourceEntries)
            {
                Assert.AreEqual((107.77 / 3), entry.Amount, 0.01);
                Assert.AreEqual(InstallmentTypeEnum.DEBIT.ToString(), entry.Type);
                Assert.AreEqual(DateTime.Now.AddMonths(i).ToShortDateString(), entry.Date.ToShortDateString());
                i += 1;
            }

            i = 0;
            foreach (Installment entry in destinationEntries)
            {
                Assert.AreEqual((107.77 / 3), entry.Amount, 0.01);
                Assert.AreEqual(InstallmentTypeEnum.CREDIT.ToString(), entry.Type);
                Assert.AreEqual(DateTime.Now.AddMonths(i).ToShortDateString(), entry.Date.ToShortDateString());
                i += 1;
            }
        }

        [TestMethod]
        public async Task PostPaymentAmountTest()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 1;

            var value = payment.Amount * (100 + _feeRepository.GetFeeByPortion(payment.NumberOfPortions).Value) / 100;

            var result = await manager.PostPayment(new PaymentViewPost(payment));
            Assert.AreEqual(value, result.NetValue);
            Assert.AreEqual(103.79, result.NetValue);
        }

        [TestMethod]
        public async Task PostPaymentAmount2Test()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 2;

            var value = payment.Amount * (100 + _feeRepository.GetFeeByPortion(payment.NumberOfPortions).Value) / 100;

            var result = await manager.PostPayment(new PaymentViewPost(payment));
            Assert.AreEqual(value, result.NetValue);
            Assert.AreEqual(105.78, result.NetValue);
        }

        [TestMethod]
        public async Task PostPaymentAmount3Test()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 3;

            var value = payment.Amount * (100 + _feeRepository.GetFeeByPortion(payment.NumberOfPortions).Value) / 100;

            var result = await manager.PostPayment(new PaymentViewPost(payment));
            Assert.AreEqual(value, result.NetValue, 0.01);
            Assert.AreEqual(107.77, result.NetValue, 0.01);
        }

        [TestMethod]
        public async Task PostPaymentDateTest()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 3;

            var result = await manager.PostPayment(new PaymentViewPost(payment));
            var lastPayment = _paymentRepository.GetLastPayment();
            Assert.AreEqual(DateTime.Now.ToShortDateString(), lastPayment.DateTime.ToShortDateString());
        }

        [TestMethod]
        public async Task PostPaymentAccountBalanceTest()
        {
            var manager = new PaymentManager(_checkingAccountRepository, _paymentRepository, _feeRepository, _entryRepository);

            Payment payment = new Payment();
            payment.SourceAccountId = 1;
            payment.DestinationAccountId = 2;
            payment.Amount = 100;
            payment.NumberOfPortions = 3;

            var sourceBalance = _checkingAccountRepository.GetAccountById(payment.SourceAccountId).Result.Balance;
            var destinationBalance = _checkingAccountRepository.GetAccountById(payment.DestinationAccountId).Result.Balance;

            var result = await manager.PostPayment(new PaymentViewPost(payment));

            var sourceBalanceAfter = _checkingAccountRepository.GetAccountById(payment.SourceAccountId).Result.Balance;
            var destinationBalanceAfter = _checkingAccountRepository.GetAccountById(payment.DestinationAccountId).Result.Balance;

            Assert.AreEqual(sourceBalance - 107.77 / 3, sourceBalanceAfter, 0.01);
            Assert.AreEqual(destinationBalance + 107.77 / 3, destinationBalanceAfter, 0.01);
            Assert.AreEqual(sourceBalance - payment.Amount * 1.0777 / 3, sourceBalanceAfter, 0.01);
            Assert.AreEqual(destinationBalance + payment.Amount * 1.0777 / 3, destinationBalanceAfter, 0.01);

        }
    }
}
