using System;

namespace Arrow.DeveloperTest.Types
{
    public interface IMakePaymentRequest
    {
        decimal Amount { get; set; }
        string CreditorAccountNumber { get; set; }
        string DebtorAccountNumber { get; set; }
        DateTime PaymentDate { get; set; }
        PaymentScheme PaymentScheme { get; set; }
    }
}