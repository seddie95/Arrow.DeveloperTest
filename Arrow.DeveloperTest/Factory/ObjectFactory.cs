using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using System;

namespace Arrow.DeveloperTest.Factory
{
    public static class ObjectFactory
    {
        /// <summary>
        /// Creates a new Instance of an Account Object
        /// </summary>
        /// <returns></returns>
        public static IAccount CreateAccount()
        {
            return new Account();
        }

        /// <summary>
        /// Creates a new Instance of an Account Object with given parameters
        /// </summary>
        /// <returns></returns>
        public static IAccount CreateAccount(string accountNumber, decimal balance, AccountStatus status, AllowedPaymentSchemes allowedPaymentSchemes)
        {
            return new Account(accountNumber, balance, status, allowedPaymentSchemes);
        }


        /// <summary>
        /// Creates a new Instance of an MakePaymentRequest Object
        /// </summary>
        /// <returns></returns>
        public static IMakePaymentRequest CreateMakePaymentRequest()
        {
            return new MakePaymentRequest();
        }

        /// <summary>
        /// Creates a new Instance of an MakePaymentRequest Object with given parameters
        /// </summary>
        /// <returns></returns>
        public static IMakePaymentRequest CreateMakePaymentRequest(string creditorAccountNumber, string debtorAccountNumber, decimal amount, DateTime paymentDate, PaymentScheme paymentScheme)
        {
            return new MakePaymentRequest(creditorAccountNumber, debtorAccountNumber, amount, paymentDate, paymentScheme);
        }

        /// <summary>
        /// Creates a new Instance of an MakePaymentRequest Object
        /// </summary>
        /// <returns></returns>
        public static IMakePaymentResult CreateMakePaymentResult()
        {
            return new MakePaymentResult();
        }

        /// <summary>
        /// Creates a new Instance of an AccountDataStore Object
        /// </summary>
        /// <returns></returns>
        public static IAccountDataStore CreateAccountDataStore()
        {
            return new AccountDataStore();
        }


        /// <summary>
        /// Creates a new Instance of an AccountService Object
        /// </summary>
        /// <returns></returns>
        public static IAccountService CreateAccountService()
        {
            return new AccountService();
        }

        /// <summary>
        /// Creates a new Instance of a PaymentService
        /// </summary>
        /// <returns></returns>
        public static IPaymentService CreatePaymentService()
        {
            return new PaymentService();
        }

        /// <summary>
        /// Creates a new Instance of a PaymentService with given parameters
        /// </summary>
        /// <returns></returns>
        public static IPaymentService CreatePaymentService(IAccountDataStore accountDataStore)
        {
            return new PaymentService(accountDataStore);
        }

        /// <summary>
        /// Creates a new Instance of a Logger
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateLogger()
        {
            return new Logger();
        }

    }
}

