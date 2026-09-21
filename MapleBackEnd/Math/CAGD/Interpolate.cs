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

        public static List<Vector> RepeatedLinearInterpolation(List<Vector> vectors, List<double> t, double min = 0, double max = 1)
        {
            List<Vector> interpolatedVectors = new List<Vector>();
            List<Vector> vectorsLeft = vectors;

            for (int i = 0; i < t.Count; i++)
            {
                for (int j = 1; j < vectorsLeft.Count; i++)
                {
                    interpolatedVectors.Add(Interpolate.StandardLinearInterpolate(vectors[j - 1], vectors[j], t[i]));
                }

                vectorsLeft = interpolatedVectors;
                interpolatedVectors = new List<Vector>();
            }

            return vectorsLeft;
        }
    }
}
