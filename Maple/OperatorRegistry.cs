using System;
using System.Collections.Generic;
using System.Text;

namespace Maple;
public class OperatorRegistry
{
    private static readonly Dictionary<(Type, Type, Operation), Func<MathObject, MathObject, MathObject>> map = new Dictionary<(Type, Type, Operation), Func<MathObject, MathObject, MathObject>>();

    private static OperatorRegistry instance;
    public static OperatorRegistry Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new OperatorRegistry();
            }

            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private OperatorRegistry()
    {
        InitalizeOperatorRegistry();
    }

    private void InitalizeOperatorRegistry()
    {
        #region Int
        #region *
        //Ia*Ib
        Add<MInt, MInt>(Operation.Multiply, (a, b) => new MInt(a.Value * b.Value));
        #endregion
        #region +
        // Ia+Ib
        Add<MInt, MInt>(Operation.Add, (a, b) => new MInt(a.Value + b.Value));
        #endregion
        #region -
        //Ia-Ib
        Add<MInt, MInt>(Operation.Subtract, (a, b) => new MInt(a.Value - b.Value));
        #endregion
        #region ^
        //Ia ^Ib
        Add<MInt, MInt>(Operation.Exponent, (a, b) => new MInt((int)System.Math.Pow((double)a.Value, (double)b.Value)));
        #endregion
        #endregion
        #region Double
        #region +
        // Da + Db
        Add<MDouble, MDouble>(Operation.Add, (a, b) => new MDouble(a.Value + b.Value));
        // Da + Ib
        Add<MDouble, MInt>(Operation.Add, (a, b) => new MDouble(a.Value + b.Value));
        // Ia + Db
        Add<MInt, MDouble>(Operation.Add, (a, b) => new MDouble(a.Value + b.Value));
        #endregion
        #region -
        //Da - Db
        Add<MDouble, MDouble>(Operation.Subtract, (a, b) => new MDouble(a.Value - b.Value));
        // Da-Ib
        Add<MDouble, MInt>(Operation.Subtract, (a, b) => new MDouble(a.Value - b.Value));
        // Ia-Db
        Add<MInt, MDouble>(Operation.Subtract, (a, b) => new MDouble(a.Value - b.Value));
        #endregion
        #region *
        // Da*Db
        Add<MDouble, MDouble>(Operation.Multiply, (a, b) => new MDouble(a.Value * b.Value));
        // Da*Ib
        Add<MDouble, MInt>(Operation.Multiply, (a, b) => new MDouble(a.Value * b.Value));
        // Ia*Db
        Add<MInt, MDouble>(Operation.Multiply, (a, b) => new MDouble(a.Value * b.Value));
        #endregion
        #region /
        //Da/Db
        Add<MDouble, MDouble>(Operation.Divide, (a, b) => new MDouble(a.Value / b.Value));
        //Da/Ib
        Add<MDouble, MInt>(Operation.Divide, (a, b) => new MDouble(a.Value / b.Value));
        // Ia/Db
        Add<MInt, MDouble>(Operation.Divide, (a, b) => new MDouble(a.Value / b.Value));
        #endregion
        #region ^
        // Da^Db
        Add<MDouble, MDouble>(Operation.Exponent, (a, b) =>
        {
            if (!MDouble.CanTakeExponent(a.Value, b.Value)) throw new ArgumentException($"Can't do {a.Value}^{b.Value}");

            return new MDouble(MathF.Pow((float)a.Value, (float)b.Value));
        });
        //Da^Ib
        Add<MDouble, MInt>(Operation.Exponent, (a, b) => new MDouble(MathF.Pow((float)a.Value, b.Value)));
        // Ia^Db
        Add<MInt, MDouble>(Operation.Exponent, (a, b) => new MDouble(MathF.Pow(a.Value, (float)b.Value)));
        #endregion
        #endregion
        #region Vector
        #region *
        // Ia * Vb
        Add<MInt, MVector>(Operation.Multiply, (a, b) => new MVector(a.Value * b.Value));
        // Da * Vb
        Add<MDouble, MVector>(Operation.Multiply, (a, b) => new MVector(a.Value * b.Value));
        #endregion
        #region /
        // Va/Ib
        Add<MVector, MInt>(Operation.Divide, (a, b) => new MVector(a.Value / b.Value));
        // Va/Db
        Add<MVector, MDouble>(Operation.Divide, (a, b) => new MVector(a.Value / b.Value));
        #endregion
        #region +
        // Va + Vb
        Add<MVector, MVector>(Operation.Add, (a, b) => new MVector(a.Value + b.Value));
        #endregion
        #region -
        //Va-Vb
        Add<MVector, MVector>(Operation.Subtract, (a, b) => new MVector(a.Value - b.Value));
        #endregion
        #endregion
    }

    private void Add<MLeft, MRight>(Operation Operator, Func<MLeft, MRight, MathObject> operation) where MLeft : MathObject where MRight : MathObject
    {
        map[(typeof(MLeft), typeof(MRight), Operator)] = (a, b) => operation((MLeft)a, (MRight)b);
    }

    public bool TryGet(Type left, Type right, Operation Operator, out Func<MathObject, MathObject, MathObject> operation) => map.TryGetValue((left, right, Operator), out operation);
}
