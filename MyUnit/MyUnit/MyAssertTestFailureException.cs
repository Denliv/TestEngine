namespace MyUnit;

public class MyAssertTestFailureException : Exception
{
    public override string Message { get; }

    public MyAssertTestFailureException() : this("")
    {
    }

    public MyAssertTestFailureException(string message)
    {
        Message = message;
    }
}