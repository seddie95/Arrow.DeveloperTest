using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Arrow.DeveloperTest.Tests
{
    public class AccountServiceTest
    {
        [Fact]
        public void ValidatePayment_ShouldReturnTrueIfSuccessful()
        {
            
            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccountService accountService = ObjectFactory.CreateAccountService();
            MakePaymentRequest paymentRequest = (MakePaymentRequest)ObjectFactory.CreateMakePaymentRequest("1234", "4567", 20, DateTime.Now, PaymentScheme.FasterPayments);

            MakePaymentResult result = accountService.ValidatePayment(paymentRequest, debtorAccount, creditorAccount);
            Assert.True(result.Success);

        }

        [Fact]
        public void ValidatePayment_ShouldReturnFalseIfAccountisNull()
        {

            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = null;
            IAccountService accountService = ObjectFactory.CreateAccountService();
            MakePaymentRequest paymentRequest = (MakePaymentRequest)ObjectFactory.CreateMakePaymentRequest("1234", "4567", 20, DateTime.Now, PaymentScheme.FasterPayments);

            MakePaymentResult result = accountService.ValidatePayment(paymentRequest, debtorAccount, creditorAccount);
            Assert.False(result.Success);

        }

        [Fact]
        public void UpdateAccountBalance_ShouldCorrectlyUpdateBalanceOfCreditor()
        {
            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccountService accountService = ObjectFactory.CreateAccountService();

            decimal amount = 10;

            decimal expectedDebtorAmount = 90;

            accountService.UpdateAccountBalance(creditorAccount, debtorAccount,amount);

            decimal actualDebtorAmount = debtorAccount.Balance;

            Assert.Equal(expectedDebtorAmount, actualDebtorAmount);

        }

        [Fact]
        public void UpdateAccountBalance_ShouldCorrectlyUpdateBalanceOfDebitor()
        {
            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccountService accountService = ObjectFactory.CreateAccountService();

            decimal amount = 10;

            decimal expectedDebtorAmount = 110;

            accountService.UpdateAccountBalance(creditorAccount, debtorAccount, amount);

            decimal actualDebtorAmount = creditorAccount.Balance;

            Assert.Equal(expectedDebtorAmount, actualDebtorAmount);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void UpdateAccountBalance_ShouldFailduetoInvalidAmount(decimal amount)
        {
            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccountService accountService = ObjectFactory.CreateAccountService();
            bool result = accountService.UpdateAccountBalance(creditorAccount, debtorAccount, amount);

            Assert.False(result);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(15)]
        public void UpdateAccountBalance_ShouldPassduetoValidAmount(decimal amount)
        {
            IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
            IAccountService accountService = ObjectFactory.CreateAccountService();
            bool result = accountService.UpdateAccountBalance(creditorAccount, debtorAccount, amount);

            Assert.True(result);

        }
    }
}
