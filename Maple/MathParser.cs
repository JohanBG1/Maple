using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Maple;
public static class MathParser
{
    private static Dictionary<string, MathObject> definitions = new Dictionary<string, MathObject>();

    public static string Parse(string expression)
    {
        char c = expression[expression.Count() - 1];

        string innerExpression = expression.Remove(expression.Count() - 1, 1);
        

        return c switch
        {
            ';' => Define(innerExpression),
            ':' => Solve(innerExpression),
            _ => ""
        };
    }

    private static string Solve(string innerExpression)
    {
        string[] parts = innerExpression.Split(' ');

        ExtractMathOperations(parts, out List<MathOperator> operations);
        
        SortOperations(ref operations);

        MathObject mathObject = operations[0].A;

        foreach (MathOperator mathOperator in operations)
        {
            mathObject = mathOperator.Calculate();
        }

        return mathObject.ToString();

        static void SortOperations(ref List<MathOperator> operations)
        {
            operations.Sort((a, b) => a.Order.CompareTo(b.Order));
            operations.Sort((a, b) => a.GetOperatorOrder().CompareTo(b.GetOperatorOrder()));
        }

        static void ExtractMathOperations(string[] parts, out List<MathOperator> operations)
        {
            operations = new List<MathOperator>();

            for (int i = 0; i < parts.Count(); i++)
            {
                if (parts[i] == "+" || parts[i] == "-")
                {
                    operations.Add(new MathOperator(parts[i], FindMathObjectWithName(parts[i - 1]), FindMathObjectWithName(parts[i + 1]), 0));
                }
            }

            SetupPointers(parts, operations);

            static void SetupPointers(string[] parts, List<MathOperator> operations)
            {
                for (int i = 0; i < operations.Count(); i++)
                {
                    if (operations.Count() == 1) return;

                    if (i == 0)
                    {
                        operations[i].MA = operations[i + 1];
                        operations[i].MB = null;
                    }

                    else if (i == operations.Count() - 1)
                    {
                        operations[i].MA = null;
                        operations[i].MB = operations[i - 1];
                    }

                    else
                    {
                        operations[i].MA = operations[i + 1];
                        operations[i].MB = operations[i - 1];
                    }
                }
            }
        }
    }

    private static string Define(string innerExpression)
    {
        string[] parts = innerExpression.Split(' ');
        TryDefine(parts[0], parts[1], parts[3]);
        return "";

        static void TryDefine(string type, string name, string value)
        {
            MathObject m = type switch
            {
                "int" => new MInt(MInt.Parse(value)),
                "vector" => new MVector(MVector.Parse(value)),
                _ => throw new ArgumentException("Can't define")
            };

            definitions[name] = m;
        }
    }

    private static MathObject FindMathObjectWithName(string name) => (from c in definitions
                                                           where c.Key == name
                                                           select c).First().Value;
}