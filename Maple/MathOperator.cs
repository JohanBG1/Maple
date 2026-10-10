using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;

public class MathOperator
{
    public MathObject A;
    public MathObject B;

    public MathOperator MA;
    public MathOperator MB;

    public string Operator { get; init; }

    public int Order;

    public int GetOperatorOrder() => Operator switch
    {
        "+" => 2,
        "-" => 2,
        "*" => 1,
        "/" => 1,
    };

    public MathObject Calculate() => Operator switch
    {
        "+" => A.Sum(A, B),
        "-" => A.Subtract(A, B),
    };

    public MathOperator(string Operator, MathObject a, MathObject b, int order)
    {
        this.Operator = Operator;
        A = a;
        B = b;
        Order = order;
    }
}
