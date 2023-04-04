using Arrow.DeveloperTest.Data;
using Arrow.DeveloperTest.Types;

using Arrow.DeveloperTest.Helper;
using System;

namespace Arrow.DeveloperTest.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            DeveloperTestHelper helper = new DeveloperTestHelper();
            helper.TestPayment("1234", "4567", 10, new DateTime(), PaymentScheme.FasterPayments);


        }
    }
}
