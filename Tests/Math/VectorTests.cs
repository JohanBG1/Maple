using Math;
using Xunit.Abstractions;
using System.Collections.Generic;

namespace Tests.Math;
public class VectorTests : LoggerTests
{
    public VectorTests(ITestOutputHelper output) : base(output) { }

    #region testData
    public static TheoryData<Vector, Vector, Vector> AdditionTestData() => new()
    {
        {new Vector(false, 1, 2), new Vector(true, 3, 4), new Vector(true, 4, 6) }
    };

    public static TheoryData<Vector, Vector, Vector> SubtractionTestData() => new()
    {
        { new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), new Vector(false, -3, -3, -3) },
        { new Vector(false, 1, 2, 3), new Vector(true, 4, 5, 6), new Vector(true, -3, -3, -3) },
        { new Vector(false, 1, 2, 3), new Vector(false, 4, 5, 6), new Vector(false, -3, -3, -3) }
    };

    public static TheoryData<double, Vector, Vector> MultiplicationTestData() => new()
    {
        { 3, new Vector(false, 3, 4, 5, 6), new Vector(false, 9, 12, 15, 18) }
    };

    public static TheoryData<List<Vector>, List<double>, Vector> AffineCombinationTestData() => new()
    {
        { new List<Vector> {new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), new Vector(true, 7, 8, 9)},
          new List<double> {0.1, 0.1, 0.8},
          new Vector(true, 6.1, 7.1, 8.1) }
    };

    public static TheoryData<Vector, Vector, double> DotTestData() => new()
    {
        {new Vector(false, 1, 2, 5), new Vector(false, 2, 3, 4), 28 }
    };
    #endregion

    [Theory]
    [MemberData(nameof(AdditionTestData))]
    public void SumTest(Vector a, Vector b, Vector expected)
    {
        Vector result = a + b;
        Assert.True(result == expected);
    }

    [Theory]
    [MemberData(nameof(SubtractionTestData))]
    public void SubtractionTest(Vector a, Vector b, Vector expected)
    {
        Vector result = a - b;

        Output.WriteLine($"Result: {result}");
        Output.WriteLine($"Expected: {expected}");
        Assert.True(result == expected);
    }
        
    [Theory]
    [MemberData(nameof(MultiplicationTestData))]
    public void MultiplicationTest(double a, Vector b, Vector expected)
    {
        Vector result = a * b;
        Assert.True(result == expected);
    }

        
    [Theory]
    [MemberData(nameof(AffineCombinationTestData))]
    public void AffineCombinationTest(List<Vector> vectors, List<double> weights, Vector expected)
    {
        List<(Vector vector, double weights)> vectorWeightSet = new List<(Vector vector, double weights)>();
        
        for (int i = 0; i < vectors.Count; i++)
        {
            vectorWeightSet.Add((vectors[i], weights[i]));
        }

        Vector result = Vector.MakeAffineCombination(vectorWeightSet);

        Output.WriteLine($"Result is {result}");
        Output.WriteLine($"Expected is {expected}");

        Assert.True(result == expected);
    }

    [Theory]
    [MemberData(nameof(DotTestData))]
    public void DotTest(Vector a, Vector b, double expected)
    {
        double result = Vector.Dot(a, b);

        Output.WriteLine($"Result: {result}");
        Output.WriteLine($"Expected: {expected}");
        Assert.True(result == expected);
    }

    [Fact]
    public void AddDifferentDimension()
    {

        Assert.Throws<ArgumentException>(() =>
        {
            Vector result = new Vector(false, 1, 2, 3) + new Vector(false, 1, 2, 3, 4);
        });
    }
}
