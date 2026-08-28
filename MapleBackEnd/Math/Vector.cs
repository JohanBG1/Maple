using System;
using System.Collections.Generic;
using System.Text;

namespace MapleBackEnd.Math
{
    public struct Vector
    {
        public List<double> Components { get; private set; }
        public bool IsPoint { get; init; }

        public Vector(bool isPoint, params double[] components)
        {
            Components = components.ToList();
            this.IsPoint = isPoint;
        }


        public int Dimension => Components.Count - 1;

        public static double Dot(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");

            double dot = 0;

            for (int i = 0; i < a.Dimension; i++)
            {
                dot += a.Components[i] * b.Components[i];
            }

            return dot;
        }

        public static operator Vector +(Vector a, Vector b)
    }
}
