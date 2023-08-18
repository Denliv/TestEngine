namespace MyUnit.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class MyInlineDataAttribute : MyAttribute
{
    public object[] Parameters { get; }
    
    public MyInlineDataAttribute(params object[] parameters)
    {
        Parameters = new object[parameters.Length];
        for (var i = 0; i < parameters.Length; i++)
        {
            Parameters[i] = parameters[i];
        }
    }
}