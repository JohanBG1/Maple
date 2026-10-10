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

    public MathObject Calculate()
    {
        MathObject m = Operator switch
        {
            "+" => A.Sum(A, B),
            "-" => A.Subtract(A, B),
        };

        if(MB != null && MA != null)
        {
            MB.B = m;
            MA.A = m;
            MB.MA = MA;
            MA.MB = MB;
        }

        else if(MB == null && MA != null)
        {
            MA.A = m;
            MA.MB = null;
        }

        else if (MB != null && MA == null)
        {
            MB.B = m;
            MB.MA = null;
        }

        return m;
    }

    public MathOperator(string Operator, MathObject a, MathObject b, int order)
    {
        this.Operator = Operator;
        A = a;
        B = b;
        Order = order;
    }
}
