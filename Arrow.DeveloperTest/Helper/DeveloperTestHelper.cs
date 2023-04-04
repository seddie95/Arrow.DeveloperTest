using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Helper
{
    public class DeveloperTestHelper
    {

        public DeveloperTestHelper()
        {
            _accountDataStore = ObjectFactory.CreateAccountDataStore();
            _logger = ObjectFactory.CreateLogger();
        }

        /// <summary>
        ///  Creates a Debitor account
        /// </summary>
        /// <returns></returns>
        public bool CreateAccounts()
        {
            try
            {
                // Create a debtor account to pay creditor and update the store
                IAccount debtorAccount = ObjectFactory.CreateAccount("1234", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
                bool addedSuccessfully = _accountDataStore.SetAccount(debtorAccount);

                IAccount creditorAccount = ObjectFactory.CreateAccount("4567", 100, AccountStatus.Live, AllowedPaymentSchemes.FasterPayments);
                addedSuccessfully = _accountDataStore.SetAccount(creditorAccount);

                if (!addedSuccessfully)
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return false;
            }

            return true;

        }

        /// <summary>
        /// Tests the payment using varying input parameters
        /// </summary>
        /// <param name="CreditorAccountNumber"></param>
        /// <param name="DebtorAccountNumber"></param>
        /// <param name="amount"></param>
        /// <param name="PaymentDate"></param>
        /// <param name="PaymentScheme"></param>
        public bool TestPayment(string creditorAccountNumber, string debtorAccountNumber, int amount, DateTime paymentDate, PaymentScheme paymentScheme)
        {
            IPaymentService paymentService = ObjectFactory.CreatePaymentService(_accountDataStore);

            // Set up the basic details for the request
            MakePaymentRequest paymentRequest = (MakePaymentRequest)ObjectFactory.CreateMakePaymentRequest(creditorAccountNumber, debtorAccountNumber, amount, paymentDate, paymentScheme);


            bool result = false;
            if (CreateAccounts())
            {
                result = paymentService.MakePayment(paymentRequest).Success;
                _logger.Info("Test payment successful: " + result);
            }

            return result;

        }

        private IAccountDataStore _accountDataStore = null;
        private ILogger _logger = null;
    }
}
