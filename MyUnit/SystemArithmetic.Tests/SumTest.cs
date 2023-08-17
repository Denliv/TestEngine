using MyUnit;
using MyUnit.Attributes;

namespace SystemArithmetic.Tests
{
    public class SumTest
    {
        [MyFact]
        public void OnePlusOne_EqualsTwo()
        {
            MyAssert.True(1 + 1 == 2);
        }
    }
}
