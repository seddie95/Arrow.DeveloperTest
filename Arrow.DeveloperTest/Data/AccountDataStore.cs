
using Arrow.DeveloperTest.Factory;
using Arrow.DeveloperTest.Services;
using Arrow.DeveloperTest.Types;
using System.Collections.Generic;

namespace Arrow.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {

        public AccountDataStore()
        {
            _logger = ObjectFactory.CreateLogger();
        }

        /// <summary>
        /// Retrieves the account from the database
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public IAccount GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            IAccount account;

            bool accountAvailable = accountDataStoreMap.TryGetValue(accountNumber, out account);

            if (!accountAvailable)
            {
                _logger.Info("Account not available");
                account = null;
            }

            return account;
        }

        private bool CanAddAccount(IAccount account)
        {
            if (account == null)
            {
                _logger.Warn("Attempting to save account which is null");
                return false;
            }
            else if (account.AccountNumber == null)
            {
                _logger.Warn("Attempting to save account without an Account number");
                return false;
            }
            else if (account.AccountNumber == string.Empty)
            {
                _logger.Warn("Attempting to save account an empty Account number");
                return false;
            }
            return true;

        }

        /// <summary>
        /// Sets the Account value in the Dictionary
        /// </summary>
        /// <param name="account"></param>
        public bool SetAccount(IAccount account)
        {
            if (!CanAddAccount(account))
            {
                return false;
            }

            if (!accountDataStoreMap.ContainsKey(account.AccountNumber))
            {
                accountDataStoreMap.Add(account.AccountNumber, account);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Updates the Account Value in the Dictionary
        /// </summary>
        /// <param name="account"></param>
        public bool UpdateAccount(IAccount account)
        {
            if (!CanAddAccount(account))
            {
                return false;
            }

            if (accountDataStoreMap.ContainsKey(account.AccountNumber))
            {
                accountDataStoreMap[account.AccountNumber] = account;
                return true;
            }
            return false;
        }


        // Dictionary to store Accounts 
        private Dictionary<string, IAccount> accountDataStoreMap = new Dictionary<string, IAccount>();
        private ILogger _logger = null;

    }
}
