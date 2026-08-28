using System;
using System.Collections.Generic;
using System.Text;

namespace Math
{
    public struct Vector
    {
        public List<double> Components { get; private set; }
        public bool IsPoint { get; init; }

        public double this[int index]
        {
            get => Components[index];
        }

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

        public static Vector operator +(Vector a, Vector b)
        {
            if(a.IsPoint == true && b.IsPoint == true) throw new ArgumentException("Cannot add two points together.");
            if(a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for addition.");

            List<double> components = new List<double>();

            for(int i = 0; i < a.Dimension; i++)
            {
                components.Add(a[i] + b[i]);
            }

            return new Vector(a.IsPoint || b.IsPoint, components.ToArray());
        }

        public static Vector operator -(Vector a, Vector b)
        {
            if(a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for subtraction.");

            List<double> components = new List<double>();

            for (int i = 0; i < a.Dimension; i++)
            {
                components.Add(a[i] - b[i]);
            }

            return new Vector(false, components.ToArray());
        }

        public static Vector operator *(Vector a, double scalar)
        {

            if(a.IsPoint == true) throw new ArgumentException("Cannot scale a point.");

            List<double> components = new List<double>();

            for (int i = 0; i < a.Dimension; i++)
            {
                components.Add(a[i] * scalar);
            }

            return new Vector(false, components.ToArray());
        }

        public static bool operator ==(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension) return false;
            if (a.IsPoint != b.IsPoint) return false;

            for (int i = 0; i < a.Dimension; i++)
            {
                if (a[i] != b[i]) return false;
            }

            return true;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }
    }
}
