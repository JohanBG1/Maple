using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Math;

using Xunit;
using Xunit.Abstractions;

namespace Tests.Math
{
    public class NumericsTests : LoggerTests
    {
        public NumericsTests(ITestOutputHelper output) : base(output) { }

        public static TheoryData<float, float, bool> AlmostEqualTestData() => new()
        {
            { 1.2455331f, 1.2455331f, true },
        };

        [Theory]
        [MemberData(nameof(AlmostEqualTestData))]
        public void AlmostEqualTest<T>(T a, T b, bool expected) where T : IFloatingPointIeee754<T>
        {
            bool result = Numerics.AlmostEqual(a, b);

            Output.WriteLine($"result is: {result}");
            Assert.True(result == expected);
        }

        public static TheoryData<float, bool> AlmostZeroTestData() => new()
        {
            {0f, true }
        };

        [Theory]
        [MemberData(nameof(AlmostZeroTestData))]
        public void AlmostZeroTest<T>(T value, bool expected) where T : IFloatingPointIeee754<T>
        {
            bool result = Numerics.AlmostZero(value);

            Assert.True(result == expected);
        }
    }
}
