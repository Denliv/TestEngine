using SystemArithmetic.Tests;
using MyUnit;

var typeInfo = typeof(ArithmeticTest);

TestRunner.OnTestFailure += (name, message) =>
    Console.WriteLine(
        $"-NOT PASSED: {name}{(string.IsNullOrWhiteSpace(message) ? string.Empty : $"\n Message: {message}")}");
TestRunner.OnTestPass += (name, message) =>
    Console.WriteLine(
        $"+PASSED: {name}");

TestRunner.Run(typeInfo);