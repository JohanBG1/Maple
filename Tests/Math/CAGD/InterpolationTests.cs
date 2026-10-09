using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Math;

namespace Tests.Math;
public class InterpolationTests : LoggerTests
{
    public InterpolationTests(ITestOutputHelper output) : base(output) { }

    #region testData
    public static TheoryData<Vector, Vector, double, double, double, Vector> LinearInterpolationTestData() => new()
    {
        {new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), 0.4, 0, 1, new Vector(true, 11.0/5.0, 16.0/5.0, 21.0/5.0) }
    };

    public static TheoryData<List<Vector>, List<double>, double, double, List<Vector>> RepeatedLinearInterpolationTestData() => new()
    {
        { new List<Vector>() { new Vector(false, 1, 2, 3), new Vector(false, 4, 5, 6), new Vector(false, 7, 8, 9), new Vector(false, 10, 11, 12)},
          new List<double>() {1, 0.5, 0.4 },
          0, 1,

         new List<Vector>() { new Vector(false, 6.7, 7.7, 8.7) } }
    };

    public static TheoryData<List<Vector>, List<double>, Vector> BlossomTestData() => new()
    {
        { new List<Vector>(){ new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), new Vector(true, 7, 8, 9) },
          new List<double>(){ 0.5, 0.1 },
          new Vector(true, 2.8, 3.8, 4.8)}
    };

    #endregion

    [Theory]
    [MemberData(nameof(LinearInterpolationTestData))]
    public void LinearInterpolationTest(Vector a, Vector b, double t, double min, double max, Vector expected)
    {
        Vector result = Interpolate.StandardLinearInterpolate(a, b, t, min, max);

        Output.WriteLine($"result is: {result}");
        Output.WriteLine($"Expected is: {expected}");

        Assert.True(result == expected);
    }

    [Theory]
    [MemberData(nameof(RepeatedLinearInterpolationTestData))]
    public void RepeatedLinearInterpolationTest(List<Vector> vectors, List<double> t, double min, double max, List<Vector> expected)
    {
        List<Vector> result = Interpolate.RepeatedLinearInterpolation(vectors, t, min, max);

        if (expected.Count != result.Count)
        {
            Output.WriteLine("There are a different number of result vectors then expected vectors");
            Assert.True(false, "there must be the samme amount of result vectors as expected vectors");
        }

        for (int i = 0; i < result.Count; i++)
        {
            Output.WriteLine($"result {i} is {result[i]} ");
            Output.WriteLine($"expected {i} is {expected[i]} ");

            Assert.True(result[i] == expected[i]);
        }
    }

 
    [Theory]
    [MemberData(nameof(BlossomTestData))]
    public void BlossomTests(List<Vector> vectors, List<double> interpolations, Vector expected)
    {
        Vector result = Interpolate.Blossom(vectors, interpolations);

        Output.WriteLine($"Result is: {result}");
        Output.WriteLine($"Expected is: {expected}");

        Assert.True(result == expected);
    }
}