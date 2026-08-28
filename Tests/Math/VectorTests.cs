using Math;
namespace Tests.Math
{
    public class VectorTests
    {
        public static TheoryData<Vector, Vector, Vector> VectorTestData() => new()
        {
            {new Vector(false, 1, 2), new Vector(true, 3, 4), new Vector(true, 4, 6) }
        };

        [Theory]
        [MemberData(nameof(VectorTestData))]
        public void AdditionTest(Vector a, Vector b, Vector excpeted)
        {
            Vector result = a + b;
            Assert.True(result == excpeted);
        }
    }
}
