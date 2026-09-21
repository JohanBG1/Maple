using System;
using System.Collections.Generic;
using System.Text;
using Math;
using Xunit.Abstractions;

namespace Tests.Math
{
    public class BlossomTest : LoggerTests
    {
        public BlossomTest(ITestOutputHelper output) : base(output) { }
        #region testData
        public static TheoryData<List<Vector>, List<double>, Vector> BlossomTestData() => new()
        {
            { new List<Vector>(){ new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), new Vector(true, 7, 8, 9) },
              new List<double>(){ 0.5, 0.1 },
              new Vector(true, 2.8, 3.8, 4.8)}
        };

        #endregion

        [Theory]
        [MemberData(nameof(BlossomTestData))]
        public void BlossomTests(List<Vector> vectors, List<double> interpolations, Vector expected)
        {
            Vector result = Blossom.GetBlossomOf(vectors, interpolations);

            Output.WriteLine($"Result is: {result}");
            Output.WriteLine($"Expected is: {expected}");

            Assert.True(result == expected);
        }
    }
}
