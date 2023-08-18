namespace MyUnit;

public static class MyAssert
{
    public static void True(bool actual)
    {
        if (!actual)
            throw new MyAssertTestFailureException($"Expected: True, but was: {actual}");
    }

    public static void False(bool actual)
    {
        if (actual)
            throw new MyAssertTestFailureException($"Expected: False, but was: {actual}");
    }

    public static void Throws(Type expectedExceptionType, Func<object?> func)
    {
        try
        {
            func.Invoke();
        }
        catch (Exception exception)
        {
            if (exception.GetType() != expectedExceptionType)
                throw new MyAssertTestFailureException(
                    $"Expected: {expectedExceptionType.Name}, but was: {exception.GetType().Name}");
        }
    }
}