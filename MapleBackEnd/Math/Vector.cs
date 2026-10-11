using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Math
{
    public struct Vector
    {
        public List<double> Components { get; set; }
        public bool IsPoint => Components[Components.Count - 1] == 1;

        public double this[int index]
        {
            get => Components[index];
            private set => Components[index] = value;
        }

        public Vector(bool isPoint, params List<double> components)
        {
            Components = components.ToList();
            Components.Add(isPoint ? 1 : 0);
        }

        public readonly int Dimension => Components.Count - 1;

        public static double Dot(Vector a, Vector b)
        {
            if(a.IsPoint == true || b.IsPoint == true) throw new ArgumentException("Cannot compute dot product with a point.");
            if (a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");

            double dot = 0;

            for (int i = 0; i <= a.Dimension -1; i++)
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

            for(int i = 0; i <= a.Dimension -1; i++)
            {
                components.Add(a[i] + b[i]);
            }

            return new Vector(a.IsPoint || b.IsPoint, components);
        }
        
        public static Vector operator -(Vector a, Vector b)
        {
            if(a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for subtraction.");
            if (a.IsPoint && !b.IsPoint) throw new ArgumentException("can't subtract point from vector");

            List<double> components = new List<double>();

            for (int i = 0; i <= a.Dimension - 1; i++)
            {
                components.Add(a[i] - b[i]);
            }
                               // any point                 / is both point
            return new Vector((a.IsPoint || b.IsPoint) && !(a.IsPoint && b.IsPoint), components);
        }


        public static Vector operator *(double scalar, Vector a)
        {
            if(a.IsPoint == true) throw new ArgumentException("Cannot scale a point.");

            List<double> components = new List<double>();

            for (int i = 0; i <= a.Dimension - 1; i++)
            {
                components.Add(a[i] * scalar);
            }

            return new Vector(false, components);
        }

        public static Vector operator /(Vector a, double scalar) => (1 / scalar) * a;
        
        public static Vector MakeAffineCombination(params List<(Vector vector, double weight)> vectorWeightSets)
        {
            if (vectorWeightSets.Count <= 0) throw new ArgumentException("Need points and weights for affine combination");
            List<Vector> vectors = new List<Vector>();
            List<double> weights = new List<double>();

            foreach((Vector vector, double weight) vectorWeightSet in vectorWeightSets)
            {
                vectors.Add(vectorWeightSet.vector);
                weights.Add(vectorWeightSet.weight);
            }

            int dimension = vectors[0].Dimension;

            foreach(Vector vector in vectors)
            {
                if (vector.Dimension != dimension) throw new ArgumentException("Can't make affine combinations with vectors that are different dimensions");
            }

            double weightSum = 0;

            for (int i = 0; i < weights.Count; i++)
            {            
                weightSum += weights[i] * vectors[i][dimension];
            }

            if (!(Numerics.AlmostZero(weightSum - 1) || Numerics.AlmostZero(weightSum))) throw new ArgumentException("The sum of the affine weights has to be 1 or 0");

            for(int i = 0; i < vectors.Count; i++)
            {
                Vector v = vectors[i];
                v[v.Dimension] = 0;
                vectors[i] = v;
            }

            double[] components = new double[vectors[0].Dimension];

            for(int i = 0; i <= dimension -1; i++)
            {
                double value = 0;

                for(int j =0; j < vectors.Count; j++)
                {
                    value += weights[j] * vectors[j][i];
                }

                components[i] = value;
            }

            return new Vector(weightSum == 0 ? false : true, components.ToList());
        }

        public static bool operator ==(Vector a, Vector b)
        {
            if (a.Dimension != b.Dimension) return false;

            for (int i = 0; i <= a.Dimension; i++)
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

                for (int i = 0; i < Dimension - 1; i++)
                {
                    result += $"{this[i]}, ";
                }
                
                result += $"{this[Dimension - 1]})";
            }

            else
            {
                result = "[";

                for (int i = 0; i < Dimension -1; i++)
                {
                    result += $" {this[i]},";
                }

                result += $"{this[Dimension - 1]}]";
            }

            return result;
        }
    }
}
