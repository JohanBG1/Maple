using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace MapleBackEnd.Math
{
    public static class Numerics 
    {

        public static T DefaultEpsilon<T>() where T :  IFloatingPointIeee754<T>
        {
            if (typeof(T) == typeof(float)) return T.CreateChecked(1e-6);
            if (typeof(T) == typeof(double)) return T.CreateChecked(1e-9);
            if (typeof(T) == typeof(Half)) return T.CreateChecked(1e-3);

            return T.CreateChecked(1e-9);
        }

        public static bool AlmostEqual<T>(T a, T b, T epsilon = default) where T : IFloatingPointIeee754<T>
        {
            epsilon = DefaultEpsilon<T>();
            T abs = T.Abs(b - a);
            return abs <= epsilon;
        }

        public static bool AlmostZero<T>(T a, T epsilon = default) where T : IFloatingPointIeee754<T>
        {
            epsilon = DefaultEpsilon<T>();

            return a <= epsilon;
        }
    }
}
