using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Arrow.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        #region Constructors
        // Constructor for no injected dependencies
        public PaymentService()
        {
            AccountDataStoreGetData = ObjectFactory.CreateAccountDataStore();
        }

        // Constructor with injected dependencies
        public PaymentService(IAccountDataStore accountDataStore)
        {
            AccountDataStoreGetData = accountDataStore;
        }

        #endregion

        #region Properties

        // Property to store the Account Data Store injected 
        public IAccountDataStore AccountDataStoreGetData
        {
            get
            {
                return accountDataStoreGetData;
            }
            private set
            {
                accountDataStoreGetData = value;
            }
        }

        private IAccountDataStore accountDataStoreGetData = null;

        #endregion
        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {

            // Obtain the debtor and creditor accounts
            IAccount debtorAccount = AccountDataStoreGetData.GetAccount(request.DebtorAccountNumber);
            IAccount creditorAccount = AccountDataStoreGetData.GetAccount(request.CreditorAccountNumber);

            IAccountService accountService = ObjectFactory.CreateAccountService();

            MakePaymentResult result = accountService.ValidatePayment(request, debtorAccount,creditorAccount);

            if (result.Success)
            {
                // Update the creditor and debitor balance and update the database
                bool balanceUpdated = accountService.UpdateAccountBalance(creditorAccount, debtorAccount, request.Amount);
                if (balanceUpdated)
                {
                    AccountDataStoreGetData.UpdateAccount(creditorAccount);
                    AccountDataStoreGetData.UpdateAccount(debtorAccount);
                }
                else
                {
                    result.Success = false;
                }
            }

            return result;
        }

    }
}
