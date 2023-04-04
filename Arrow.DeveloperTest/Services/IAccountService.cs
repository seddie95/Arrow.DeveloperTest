using Arrow.DeveloperTest.Types;
using System.Collections.Generic;

namespace Arrow.DeveloperTest.Services
{
    public interface IAccountService
    {
        bool UpdateAccountBalance(IAccount creditorAccount, IAccount debtorAccount, decimal paymentAmount);

        MakePaymentResult ValidatePayment(MakePaymentRequest request, IAccount debtorAccount, IAccount creditorAccount);
    }
}