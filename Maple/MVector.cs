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
        this.Vector = vector;
    }

    public Vector Vector { get; private set; }

    internal static Vector Parse(string value)
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
                
            return new Vector(false, results);
        }

        throw new ArgumentException("Wrong setup");
    }

    public MathObject Subtract(MathObject a, MathObject b)
    {
        if (!(a is MVector aVector && b is MVector bVector)) throw new ArgumentException("Only works for vectors");

        return new MVector(aVector.Vector - bVector.Vector);
    }

    public MathObject Sum(MathObject a, MathObject b)
    {
        if (!(a is MVector aVector && b is MVector bVector)) throw new ArgumentException("Only works for vectors");

        return new MVector(aVector.Vector + bVector.Vector);
    }

    public override string ToString() => Vector.ToString();
}