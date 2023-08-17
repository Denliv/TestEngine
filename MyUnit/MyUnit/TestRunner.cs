using System;
using System.Linq;
using System.Reflection;
using MyUnit.Attributes;

namespace MyUnit
{
    public static class TestRunner
    {
        public static event TestRunEventHandler OnTestPass;
        public static event TestRunEventHandler OnTestFailure;

        public static void Run(Type sourceType)
        {
            var methods = sourceType
                .GetMethods()
                .Where(o => o.GetCustomAttribute(typeof(MyFactAttribute)) is not null);

            if (!sourceType.GetConstructors().Any(o => o.GetParameters().Length == 0))
                throw new InvalidOperationException($"В типе {sourceType.FullName} необходим конструктор без параметров");

            var instance = Activator.CreateInstance(sourceType)!;

            foreach (var method in methods)
            {
                if (method.GetParameters().Any())
                    throw new InvalidOperationException($"Тестовый метод, отмеченный атрибутом {nameof(MyFactAttribute)}, не должен принимать параметры");

                try
                {
                    method.Invoke(instance, null);
                }
                catch (TargetInvocationException ex) when (ex.InnerException is MyAssertTestFailureException)
                {
                    OnTestFailure?.Invoke($"{sourceType.Name}.{method.Name}");
                }
            }
        }
    }
}
