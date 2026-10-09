using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;

public class MInt : MathObject
{
    public MInt(int value)
    {
        this.Value = value;
    }

    public int Value { get; private set; }

    public MathObject Subtract(MathObject a, MathObject b)
    {
        if (!(a is MInt aInt && b is MInt bInt)) throw new ArgumentException("Only works for ints");

        return new MInt(aInt.Value - bInt.Value);
    }

    public MathObject Sum(MathObject a, MathObject b)
    {
        if (!(a is MInt aInt && b is MInt bInt)) throw new ArgumentException("Only works for ints");

        return new MInt(aInt.Value + bInt.Value);
    }

    public static int Parse(string value) => int.Parse(value);

    public override string ToString() => Value.ToString();
}