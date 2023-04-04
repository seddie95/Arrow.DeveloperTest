using Arrow.DeveloperTest.Types;

namespace Arrow.DeveloperTest.Data
{
    public interface IAccountDataStore
    {
        IAccount GetAccount(string accountNumber);
        bool SetAccount(IAccount account);
        bool UpdateAccount(IAccount account);
    }
}