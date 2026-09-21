using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    public static class Blossom
    {
        public static Vector GetBlossomOf(List<Vector> vectors, List<double> interpolations)
        {
            bool isPoint = vectors[0].IsPoint;

            foreach (Vector vector in vectors)
            {
                if (vector.IsPoint != isPoint) throw new ArgumentException("Can't interpolate points and vectors in the same interpolation");
            }

            if (interpolations.Count + 1 != vectors.Count) throw new ArgumentException("Can't interpolate more then or less then n-1 times for n vectors");

            List<Vector> vectorsLeft = new List<Vector>();

            foreach (Vector vector in vectors)
            {
                vectorsLeft.Add(vector);
            }

            while (vectorsLeft.Count > 1)
            {
                int interpolationTime = 0;
                List<Vector> interpolatedPoints = new List<Vector>();

                for (int i = 1; i < vectorsLeft.Count; i++)
                {
                    interpolatedPoints.Add(Interpolate.StandardLinearInterpolate(vectors[i - 1], vectors[i], interpolations[interpolationTime]));
                }

                vectorsLeft = interpolatedPoints;
                interpolatedPoints = new List<Vector>();
                interpolationTime++;
            }

            return new Vector(isPoint, vectorsLeft[0].Components);
        }
    }
}
