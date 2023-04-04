using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Arrow.DeveloperTest.Services
{
    public class AccountService : IAccountService
    {
        public AccountService()
        {
            _logger = ObjectFactory.CreateLogger();
        }
        /// <summary>
        /// Checks whether the Payment is valid 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="account"></param>
        /// <returns> MakePaymentResult </returns>
        public MakePaymentResult ValidatePayment(MakePaymentRequest request, IAccount debtorAccount, IAccount creditorAccount)
        {
            MakePaymentResult output = new MakePaymentResult();

            // return false if null
            if (debtorAccount == null || creditorAccount == null)
            {
                _logger.Warn("One of either debtor or creditor accounts are null");
                output.Success = false;
                return output;
            }

            _logger.Info("validating payment for debtor account number: " + debtorAccount.AccountNumber + " and creditor account number: " + creditorAccount.AccountNumber);

            switch (request.PaymentScheme)
            {
                case PaymentScheme.Bacs:
                    if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                    {
                        _logger.Warn(debtorAccount.AccountNumber + " does not have AllowedPaymentSchemes flag for Bacs" + "\nAllowedPaymentSchemes: " + debtorAccount.AllowedPaymentSchemes);
                        output.Success = false;
                        return output;
                    }
                    // set valid payment
                    else
                    {
                        output.Success = true;
                    }
                    break;

                case PaymentScheme.FasterPayments:
                    if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
                    {
                        _logger.Warn(debtorAccount.AccountNumber + " does not have AllowedPaymentSchemes flag for FasterPayments" + "\nAllowedPaymentSchemes: " + debtorAccount.AllowedPaymentSchemes);
                        output.Success = false;
                        return output;
                    }
                    else if (debtorAccount.Balance < request.Amount)
                    {

                        _logger.Warn(debtorAccount.AccountNumber + " has a balance of: " + debtorAccount.Balance + " which is less than the requested amount: " + request.Amount);
                        output.Success = false;
                        return output;
                    }
                    // set valid payment
                    else
                    {
                        output.Success = true;
                    }
                    break;

                case PaymentScheme.Chaps:
                    if (!debtorAccount.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                    {
                        _logger.Warn(debtorAccount.AccountNumber + " does not have AllowedPaymentSchemes flag for Chaps" + "\nAllowedPaymentSchemes: " + debtorAccount.AllowedPaymentSchemes);
                        output.Success = false;
                        return output;
                    }
                    else if (debtorAccount.Status != AccountStatus.Live)
                    {
                        _logger.Warn(debtorAccount.AccountNumber + " with AllowedPaymentSchemes flag for Chaps AccountStatus is not live " + "\nAccountStatus: " + debtorAccount.Status);
                        output.Success = false;
                        return output;
                    }
                    // set valid payment
                    else
                    {
                        output.Success = true;
                    }
                    break;
            }

            string success = output.Success ? "successful" : "failed";

            _logger.Info("validation " + success + " for debtor account number: " + debtorAccount.AccountNumber + " and creditor account number: " + creditorAccount.AccountNumber);
            return output;
        }


        /// <summary>
        /// Updates the balance for the creditor and debitor
        /// </summary>
        /// <param name="creditorAccount"></param>
        /// <param name="debtorAccount"></param>
        /// <param name="paymentAmount"></param>
        /// <returns></returns>
        public bool UpdateAccountBalance(IAccount creditorAccount, IAccount debtorAccount, decimal paymentAmount)
        {
            bool isSuccessful = true;

            if (creditorAccount == null || debtorAccount == null)
            {
                _logger.Warn("One of either debtor or creditor accounts are null");
                isSuccessful = false;
            }

            else if (paymentAmount <= 0)
            {
                _logger.Warn("Amount is less than or equal to zero");
                isSuccessful = false;
            }

            debtorAccount.Balance -= paymentAmount;
            creditorAccount.Balance += paymentAmount;

            return isSuccessful;
        }

        private ILogger _logger = null;
    }
}
