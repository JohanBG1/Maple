using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;

public interface MathObject
{
    public MathObject Sum(MathObject a, MathObject b);
    public MathObject Subtract(MathObject a, MathObject b);
}