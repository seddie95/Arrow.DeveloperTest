using System;
using System.Collections.Generic;
using System.Text;
using Arrow.DeveloperTest;
using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Types;
using Xunit;


namespace Arrow.DeveloperTest.Tests
{
    public class AccountDataStoreTest
    {
        /// <summary>
        /// Checks whether the GetAccount method returns an object that can be cast to Account
        /// </summary>
        [Theory]
        [InlineData("1234")]
        public void GetAccount_ShouldRetrieveCorrectAccount(string accountNumber)
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = ObjectFactory.CreateAccount();
            account.AccountNumber = accountNumber;

            accountDataStore.SetAccount(account);

            IAccount actualAccount = accountDataStore.GetAccount(accountNumber);

            Assert.Equal(actualAccount.AccountNumber, accountNumber);

        }

        // <summary>
        /// Checks whether the SetAccount adds a new account successfully
        /// </summary>
        [Theory]
        [InlineData("1234")]
        [InlineData("456")]
        public void SetAccount_ShouldAddNewAccountSuccessfully(string accountNumber)
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = ObjectFactory.CreateAccount();
            account.AccountNumber = accountNumber;

            bool result = accountDataStore.SetAccount(account);
            Assert.True(result);
        }

        // <summary>
        /// Checks whether the SetAccount can add the same account number twice
        /// </summary>
        [Theory]
        [InlineData("1234")]
        [InlineData("456")]
        public void SetAccount_ShouldFailAddingSameAccount(string accountNumber)
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = ObjectFactory.CreateAccount();
            account.AccountNumber = accountNumber;

            accountDataStore.SetAccount(account);
            bool result = accountDataStore.SetAccount(account);
            Assert.False(result);
        }

        // <summary>
        /// Checks whether the SetAccount can add null account
        /// </summary>
        [Fact]
        public void SetAccount_ShouldFailAddingNullAccount()
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = null;
            bool result = accountDataStore.SetAccount(account);
            Assert.False(result);
        }

        // <summary>
        /// Checks whether the SetAccount can add account without account number
        /// </summary>
        [Fact]
        public void SetAccount_ShouldFailAddingAccountWithoutAccountNumber()
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = ObjectFactory.CreateAccount(); 
            bool result = accountDataStore.SetAccount(account);
            Assert.False(result);
        }

        // <summary>
        /// Checks whether the SetAccount can add account wit Empty string for account number
        /// </summary>
        [Fact]
        public void SetAccount_ShouldFailAddingAccountWithEmptyAccountNumber()
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();
            IAccount account = ObjectFactory.CreateAccount();
            account.AccountNumber = String.Empty;
            bool result = accountDataStore.SetAccount(account);
            Assert.False(result);
        }

        [Fact]
        public void GetAccount_ShouldReturnNull()
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();

            IAccount actualAccount = accountDataStore.GetAccount("");

            Assert.Null(actualAccount);
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("456")]
        public void UpdateAccount_ShouldUpdateSuccessfully(string accountNumber)
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();

            IAccount account = ObjectFactory.CreateAccount(accountNumber,100,AccountStatus.InboundPaymentsOnly,AllowedPaymentSchemes.Bacs);
            accountDataStore.SetAccount(account);

            IAccount dbAcc = accountDataStore.GetAccount(accountNumber);
            dbAcc.Balance = 120;
            bool result = accountDataStore.UpdateAccount(dbAcc);
            Assert.True(result);
            
        }

        [Theory]
        [InlineData("1234")]
        [InlineData("456")]
        public void UpdateAccount_ShouldFailUpdating(string accountNumber)
        {
            IAccountDataStore accountDataStore = ObjectFactory.CreateAccountDataStore();

            IAccount account = ObjectFactory.CreateAccount(accountNumber, 100, AccountStatus.InboundPaymentsOnly, AllowedPaymentSchemes.Bacs);

            bool result = accountDataStore.UpdateAccount(account);
            Assert.False(result);

        }
    }
}
