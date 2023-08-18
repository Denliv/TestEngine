namespace MyUnit.Attributes;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class MyInlineDataAttribute : MyAttribute
{
    public object[] Parameters { get; }
    
    public MyInlineDataAttribute(object parameter, params object[] parameters)
    {
        Parameters = new object[parameters.Length + 1];
        Parameters[0] = parameter;
        for (var i = 0; i < parameters.Length; i++)
        {
            Parameters[i + 1] = parameters[i];
        }
    }
}