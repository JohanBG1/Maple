using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;

public class MDouble : MathObject
{
    public MDouble(double value)
    {
        Value = value;
    }

    public double Value { get; init; }

    public static double Parse(string value) => double.Parse(value);

    public override string ToString() => Value.ToString();

    #region MathOperationChecks
    public static bool CanTakeExponent(double a, double exp)
    {
        if (System.Math.Pow(exp, -1) % 2 == 0)
        {
            return a >= 0;
        }

        return true;
    }

    #endregion
}