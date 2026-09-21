using MapleBackEnd.Math;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Math
{
    public struct Vector
    {
        public List<double> Components { get; private set; }
        public readonly bool IsPoint { get; init; }

        public double this[int index]
        {
            get => Components[index];
        }

        public Vector(bool isPoint, params double[] components)
        {
            Components = components.ToList();
            this.IsPoint = isPoint;
        }


        public readonly int Dimension => Components.Count - 1;

        public static double Dot(Vector a, Vector b)
        {
            if(a.IsPoint == true || b.IsPoint == true) throw new ArgumentException("Cannot compute dot product with a point.");
            if (a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");

            double dot = 0;

            for (int i = 0; i <= a.Dimension; i++)
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

            for(int i = 0; i <= a.Dimension; i++)
            {
                components.Add(a[i] + b[i]);
            }

            return new Vector(a.IsPoint || b.IsPoint, components.ToArray());
        }
        
        public static Vector operator -(Vector a, Vector b)
        {
            if(a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for subtraction.");

            List<double> components = new List<double>();

            for (int i = 0; i <= a.Dimension; i++)
            {
                components.Add(a[i] - b[i]);
            }
                               // any point                 / is both point
            return new Vector((a.IsPoint || b.IsPoint) && !(a.IsPoint && b.IsPoint), components.ToArray());
        }
        

        public static Vector operator *(double scalar, Vector a)
        {

            if(a.IsPoint == true) throw new ArgumentException("Cannot scale a point.");

            List<double> components = new List<double>();

            for (int i = 0; i <= a.Dimension; i++)
            {
                components.Add(a[i] * scalar);
            }

            return new Vector(false, components.ToArray());
        }
        
        public static Vector MakeAffineCompination(List<Vector> vectors, List<double> weights)
        {
            #region argument Exceptions
            int dimension = vectors[0].Dimension;

            foreach (Vector vector in vectors)
            {
                if (vector.IsPoint == false) throw new ArgumentException("Can only make affine combination with points");
                if (vector.Dimension != dimension) throw new ArgumentException("Can't make affine combination with different dimensions");
            }

            double weightSum = 0;

            foreach (double weight in weights)
            {
                weightSum += weight;
            }

            if (!Numerics.AlmostZero(weightSum - 1)) throw new ArgumentException("The sum of the affine weights has to be 1");
            #endregion

            double[] components = new double[vectors[0].Dimension + 1];

            for (int i = 0; i < vectors.Count; i++)
            {
                double value = 0;

                for (int j = 0; j <= vectors[0].Dimension; j++)
                {
                    value += weights[j] * vectors[j][i];
                }

                components[i] = value;
            }

            return new Vector(true, components);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension) return false;
            if (a.IsPoint != b.IsPoint) return false;

            for (int i = 0; i < a.Dimension; i++)
            {
                if (!Numerics.AlmostEqual(a[i], b[i])) return false;
            }

            return true;
        }

        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public override string ToString()
        {
            string result = "";
            if (IsPoint)
            {
                result = "(";

                for (int i = 0; i < Dimension; i++)
                {
                    result += $"{this[i]}, ";
                }
                
                result += $"{this[Dimension]})";
            }

            

            else
            {
                for (int i = 0; i <= Dimension; i++)
                {
                    result += $"[{this[i]}] \n";
                }
            }

            return result;
        }
    }
}
