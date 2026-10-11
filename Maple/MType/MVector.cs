using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;
using Math;

namespace Maple;

public class MVector : MathObject
{
    public MVector(Vector vector)
    {
        this.Value = vector;
    }

    public Vector Value { get; init; }

    public static Vector Parse(string value)
    {

        string inner = value;
        if (value[0] == '[' || value[0] == '(')
        {
            inner = inner.Remove(0, 1);
            inner = inner.Remove(inner.Count() - 1, 1);
        }

        string pattern = @"[+-]?(?:\d+(?:\.\d*)?|\.\d+)(?:[eE][+-]?\d+)?";
        Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);
            
        List<double> results = new List<double>();

        foreach (Match m in rx.Matches(inner))
        {
            if (double.TryParse(m.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out var d))
            {
                results.Add(d);
            }

            else
            {
                throw new ArgumentException($"{m.Value} is not a double");
            }
                
        }
        
        return new Vector(false, results);
    }

    public override string ToString() => Value.ToString();
}