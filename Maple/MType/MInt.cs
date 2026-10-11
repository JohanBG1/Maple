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

    public int Value { get; init; }

    public static int Parse(string value) => int.Parse(value);

    public override string ToString() => Value.ToString();

}