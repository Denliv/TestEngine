using MyUnit;
using MyUnit.Attributes;

namespace SystemArithmetic.Tests;

public class ArithmeticTest
{
    [MyFact]
    public void OnePlusOne_EqualsTwo_Test()
    {
        MyAssert.True(1 + 1 == 2);
    }

    [MyFact]
    public void OnePlusOne_NotEqualsThree_Test()
    {
        MyAssert.False(1 + 1 == 3);
    }

    [MyFact]
    public void OneDivZero_ThrowException_Test()
    {
        int zero = 0;
        int one = 1;
        MyAssert.Throws(typeof(DivideByZeroException), () => one / zero);
    }

    [MyInlineData(1, 1, 2)]
    public void OnePlusOne_EqualsTwo_InlineTest(int a, int b, int c)
    {
        MyAssert.True(a + b == c);
    }

    [MyInlineData(1, 1, 2)]
    [MyInlineData(1, 1, 2)]
    public void OnePlusOne_EqualsTwo_SeveralInlineTest(int a, int b, int c)
    {
        MyAssert.True(a + b == c);
    }

    [MyFact]
    public void True_FailTest()
    {
        MyAssert.True(1 + 1 == 3);
    }

    [MyFact]
    public void False_FailTest()
    {
        MyAssert.False(1 + 2 == 3);
    }

    [MyFact]
    public void Throws_FailTest()
    {
        int zero = 0;
        int one = 1;
        MyAssert.Throws(typeof(ArithmeticException), () => one / zero);
    }

    //Раскомментировать для проверки конфликта MyFact и MyInlineData
    /*
    [MyFact]
    [MyInlineData(1)]
    [MyInlineData(1)]
    public void MyFact_And_MyInlineData_Conflict()
    {
        MyAssert.True(1 + 1 == 2);
    }
    
    [MyFact]
    [MyInlineData(1)]
    [MyInlineData(1)]
    public void MyFact_And_MyInlineData_Conflict(int a)
    {
        MyAssert.True(1 + a == 2);
    }*/
}