using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;
public static class MathParser
{
    private static Dictionary<string, MathObject> definitions = new Dictionary<string, MathObject>();

    public static string Parse(string expression)
    {
        char c = expression[expression.Count() - 1];
        string[] parts = expression.Split(' ');

        if (c == ';')
        {
            TryDefine(parts[0], parts[1], parts[3].Remove(parts[3].Count() - 1, 1));
            return "";
        }

        else if (c == ':')
        {
            List<MathObject> mathObjects = new List<MathObject>();
            List<string> operations = new List<string>();

            ExtractMathObjectsAndOperations(parts, mathObjects, operations);

            MathObject result = mathObjects[0];

            for(int i = 0; i < mathObjects.Count()-1; i++)
            {
                result = operations[i] switch
                {
                    "+" => mathObjects[i].Sum(mathObjects[i], mathObjects[i + 1]),
                    "-" => mathObjects[i].Subtract(mathObjects[i], mathObjects[i + 1])
                };
            }

            return result.ToString();
        }

        return "";

        static void ExtractMathObjectsAndOperations(string[] parts, List<MathObject> mathObjects, List<string> operations)
        {
            for (int i = 0; i < parts.Count(); i++)
            {
                if (i % 2 == 0)
                {
                    MathObject m;

                    if (i == parts.Count() - 1)
                    {
                        m = FindMathObjectWithName(parts[i].Remove(parts[i].Count() - 1, 1));
                    }

                    else
                    {
                        m = FindMathObjectWithName(parts[0]);
                    }

                    mathObjects.Add(m);
                }

                if (i % 2 == 1)
                {
                    operations.Add(parts[i]);
                }
            }
        }
    }

    private static void TryDefine(string type, string name, string value)
    {
        MathObject m = type switch
        {
            "int" => new MInt(MInt.Parse(value)),
            "vector" => new MVector(MVector.Parse(value)),
            _ => throw new ArgumentException("Can't define")
        };

        definitions[name] = m;
    }

    private static MathObject FindMathObjectWithName(string name) => (from c in definitions
                                                               where c.Key == name
                                                               select c).First().Value;
}