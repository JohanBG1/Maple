using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    public static class Interpolate
    {
        public static Vector StandardLinearInterpolate(Vector a, Vector b, double t, double min = 0, double max = 1)
        {
            if (a.IsPoint && !b.IsPoint) throw new ArgumentException("Can't linear interpolate point and vector");
            
            return Vector.MakeAffineCombination((a, (max - t) / (max - min)), (b, (t - min) / (max - min)));
        }

        public static List<Vector> RepeatedLinearInterpolation(List<Vector> vectors, List<double> interpolations, double min = 0, double max = 1)
        {
            bool isPoint = vectors[0].IsPoint;

            foreach (Vector vector in vectors)
            {
                if (vector.IsPoint != isPoint) throw new ArgumentException("Can't interpolate points and vectors in the same interpolation");
            }


            if (interpolations.Count >= vectors.Count) throw new ArgumentException("Can't interpolate more then n-1 times where n is count of points");


            List<Vector> interpolatedVectors = new List<Vector>();
            List<Vector> vectorsLeft = new List<Vector>(vectors);

            for (int i = 0; i < interpolations.Count; i++)
            {
                for (int j = 1; j < vectorsLeft.Count; j++)
                {
                    interpolatedVectors.Add(StandardLinearInterpolate(vectorsLeft[j - 1], vectorsLeft[j], interpolations[i], min, max));
                }

                vectorsLeft = interpolatedVectors;
                interpolatedVectors = new List<Vector>();
            }

            return vectorsLeft;
        }

        public static Vector Blossom(List<Vector> vectors, List<double> interpolations, double min = 0, double max = 1)
        {
            if (interpolations.Count + 1 != vectors.Count) throw new ArgumentException("Can't interpolate more then or less then n-1 times for n vectors");

            return RepeatedLinearInterpolation(vectors, interpolations, min, max)[0];
        }
    }
}
