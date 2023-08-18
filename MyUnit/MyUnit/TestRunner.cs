using System.Reflection;
using MyUnit.Attributes;

namespace MyUnit;

public static class TestRunner
{
    public static event TestRunEventHandler OnTestPass = delegate { };
    public static event TestRunEventHandler OnTestFailure = delegate { };

    public static void Run(Type sourceType)
    {
        var methods = sourceType
            .GetMethods()
            .Where(o => o.GetCustomAttributes(typeof(MyAttribute)).Any());
        if ((sourceType == typeof(MyFactAttribute) &&
             sourceType.GetConstructors().All(o => o.GetParameters().Length != 0)) ||
            (sourceType == typeof(MyInlineDataAttribute) &&
             sourceType.GetConstructors().All(o => o.GetParameters().Length == 0)))
            throw new InvalidOperationException(
                $"In type {sourceType.FullName} should be constrictor with{(sourceType == typeof(MyFactAttribute) ? "out" : "")} parameters");

        var instance = Activator.CreateInstance(sourceType)!;
        foreach (var method in methods)
        {
            var myFactAttribute = method.GetCustomAttribute(typeof(MyFactAttribute));
            var myInlineDataAttributes = (MyInlineDataAttribute[])method.GetCustomAttributes<MyInlineDataAttribute>();
            if (myInlineDataAttributes.Any() && myFactAttribute is not null)
                throw new InvalidOperationException(
                    $"Test method with attribute {nameof(MyFactAttribute)}, should not have parameters");
            try
            {
                if (myFactAttribute is not null) method.Invoke(instance, null);
                else if (myInlineDataAttributes.Any())
                {
                    foreach (var attribute in myInlineDataAttributes)
                    {
                        method.Invoke(instance, attribute.Parameters);
                    }
                }

                OnTestPass?.Invoke($"{sourceType.Name}.{method.Name}");
            }
            catch (TargetInvocationException ex) when (ex.InnerException is MyAssertTestFailureException)
            {
                OnTestFailure?.Invoke($"{sourceType.Name}.{method.Name}", ex.InnerException.Message);
            }
        }
    }
}