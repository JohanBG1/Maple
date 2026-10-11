using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;

public enum Operation
{
    Add,
    Subtract,
    Divide,
    Multiply,
    Exponent,
}

public static class OperationHelper
{
    public static string ConvertFromOperationToString(Operation operation) => operation switch
    {
        Operation.Add => "+",
        Operation.Subtract => "-",
        Operation.Divide => "/",
        Operation.Multiply => "*",
        Operation.Exponent => "^",
    };

    public static Operation ConvertFromStringToOperation(string operation) => operation switch
    {
        "+" => Operation.Add,
        "-" => Operation.Subtract,
        "*" => Operation.Multiply,
        "/" => Operation.Divide,
        "^" => Operation.Exponent,
        _ => throw new ArgumentException($"{operation} is not an operation")
    };

    public static int GetOperatorOrder(Operation operation) => operation switch
    {
        Operation.Add => 3,
        Operation.Subtract => 3,
        Operation.Multiply => 2,
        Operation.Divide => 2,
        Operation.Exponent => 1,
    };
}