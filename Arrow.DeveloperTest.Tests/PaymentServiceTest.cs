using Arrow.DeveloperTest.Helper;
using Arrow.DeveloperTest.Types;
using System;
using Xunit;

namespace Arrow.DeveloperTest.Tests
{
    public class PaymentServiceTest
    {

        /// <summary>
        /// Test should pass as all the variables are correct 
        /// </summary>

        [Fact]
        public void MakePayment_ShouldSuccessfulyMakePayment()
        {
            DeveloperTestHelper helper = new DeveloperTestHelper();
            bool result = helper.TestPayment("1234", "4567", 10, new DateTime(), PaymentScheme.FasterPayments);

            Assert.True(result);
        }

        /// <summary>
        /// Test should fail as all the PaymentScheme is inccorect
        /// </summary>
        [Theory]
        [InlineData(PaymentScheme.Bacs)]
        [InlineData(PaymentScheme.Chaps)]
        public void MakePayment_ShouldFailToMakePaymentBasedOnIncorrectPaymentScheme(PaymentScheme paymentScheme)
        {
            DeveloperTestHelper helper = new DeveloperTestHelper();
            bool result = helper.TestPayment("1234", "4567", 10, new DateTime(), paymentScheme);

            Assert.False(result);
        }

        /// <summary>
        /// Test should fail as all the PaymentScheme is inccorect
        /// </summary>
        [Theory]
        [InlineData("478")]
        [InlineData("1578")]
        public void MakePayment_ShouldFailToMakePaymentBasedOnIncorrectDebtorAccount(string debtorAccount)
        {
            DeveloperTestHelper helper = new DeveloperTestHelper();
            bool result = helper.TestPayment("12345", debtorAccount, 10, new DateTime(), PaymentScheme.FasterPayments);

            Assert.False(result);
        }

        /// <summary>
        /// Test should fail as all the PaymentScheme is inccorect
        /// </summary>
        [Theory]
        [InlineData("1045")]
        [InlineData("78")]
        public void MakePayment_ShouldFailToMakePaymentBasedOnIncorrectCreditorAccount(string creditorAccount)
        {
            DeveloperTestHelper helper = new DeveloperTestHelper();
            bool result = helper.TestPayment(creditorAccount, "456", 10, new DateTime(), PaymentScheme.FasterPayments);

            Assert.False(result);
        }


    }
}
