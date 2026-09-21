using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;
using Math;

namespace Tests.Math
{
    public class InterpolationTests : LoggerTests
    {
        public InterpolationTests(ITestOutputHelper output) : base(output) { }

        #region testData
        public static TheoryData<Vector, Vector, double, Vector> LinearInterpolationTestData() => new()
        {
            {new Vector(true, 1, 2, 3), new Vector(true, 4, 5, 6), 0.4, new Vector(true, 3.0, 4.2, 5.4) }
        };

        #endregion
    }
}
