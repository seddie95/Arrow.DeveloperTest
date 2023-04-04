using System;

namespace Arrow.DeveloperTest.Types
{
    public class Account : IAccount
    {

        public Account()
        {

        }

        public Account(string accountNumber, decimal balance, AccountStatus status, AllowedPaymentSchemes allowedPaymentSchemes)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Status = status;
            AllowedPaymentSchemes = allowedPaymentSchemes;
        }

        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public AccountStatus Status { get; set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }


    }
}
