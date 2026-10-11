
namespace Maple;

public class MathOperator
{
    public MathObject A;
    public MathObject B;

    public MathOperator MA;
    public MathOperator MB;

    public Operation Operator { get; init; }

    public int Order;

    public int GetOperatorOrder() => OperationHelper.GetOperatorOrder(Operator);

    public MathObject Calculate()
    {
        MathObject m = (A, B, Operator) switch
        {
        #region Int
        #region *
        //Ia*Ib
        (MInt aInt, MInt bInt, Operation.Multiply) => new MInt(aInt.Value * bInt.Value),
        #endregion
        #region +
        //Ia+Ib
        (MInt aInt, MInt bInt, Operation.Add) => new MInt(aInt.Value + bInt.Value),
        #endregion
        #region -
        //Ia-Ib
        (MInt aInt, MInt bInt, Operation.Subtract) => new MInt(aInt.Value - bInt.Value),
        #endregion
        #endregion

        #region Vector
        #region *
        //Ia*Vb
        (MInt aInt, MVector bVector, Operation.Multiply) => new MVector((double)aInt.Value * bVector.Value),
        //Da*Vb
        (MDouble aDouble, MVector bVector, Operation.Multiply) => new MVector(aDouble.Value * bVector.Value),
        #endregion
        #region /
        //Va/Ib
        (MVector aVector, MInt bInt, Operation.Divide) => new MVector(aVector.Value / (double)bInt.Value),
        //Va/Db
        (MVector aVector, MDouble bDouble, Operation.Divide) => new MVector(aVector.Value / bDouble.Value),
        #endregion
        #region +
        //Va + Vb
        (MVector aVector, MVector bVector, Operation.Add) => new MVector(aVector.Value + bVector.Value),
        #endregion
        #region -
        //Va - Vb
        (MVector aVector, MVector bVector, Operation.Subtract) => new MVector(aVector.Value - bVector.Value),
        #endregion
        #endregion

        #region Double
        #region +
        // double
        //Da + Db
        (MDouble aDouble, MDouble bDouble, Operation.Add) => new MDouble(aDouble.Value + bDouble.Value),
        //Da + Ib
        (MDouble aDouble, MInt bInt, Operation.Add) => new MDouble(aDouble.Value + bInt.Value),
        //Ia + Db
        (MInt aInt, MDouble bDouble, Operation.Add) => new MDouble(aInt.Value + bDouble.Value),
        #endregion
        #region -
        //Da-Db
        (MDouble aDouble, MDouble bDouble, Operation.Subtract) => new MDouble(aDouble.Value - bDouble.Value),
        //Da-Ib
        (MDouble aDouble, MInt bInt, Operation.Subtract) => new MDouble(aDouble.Value - bInt.Value),
        //Ia-Db
        (MInt aInt, MDouble bDouble, Operation.Subtract) => new MDouble(aInt.Value - bDouble.Value),
        #endregion
        #region *
        //Da*Db
        (MDouble aDouble, MDouble bDouble, Operation.Multiply) => new MDouble(aDouble.Value * bDouble.Value),
        //Da*Ib
        (MDouble aDouble, MInt bInt, Operation.Multiply) => new MDouble(aDouble.Value * bInt.Value),
        //Ia*Db
        (MInt aInt, MDouble bDouble, Operation.Multiply) => new MDouble(aInt.Value * bDouble.Value),
        #endregion
        #region /
        //Da/Db
        (MDouble aDouble, MDouble bDouble, Operation.Divide) => new MDouble(aDouble.Value / bDouble.Value),
        //Da/Ib
        (MDouble aDouble, MInt bInt, Operation.Divide) => new MDouble(aDouble.Value / bInt.Value),
        //Ia/Db
        (MInt aInt, MDouble bDouble, Operation.Divide) => new MDouble(aInt.Value / bDouble.Value),
        #endregion
        #region ^
        // Da ^ Db
        (MDouble aDouble, MDouble bDouble, Operation.Exponent) => new MDouble(MathF.Pow((float)aDouble.Value, (float)bDouble.Value)),
        // Da^Ib
        (MDouble aDouble, MInt bInt, Operation.Exponent) => new MDouble(MathF.Pow((float)aDouble.Value, (float)bInt.Value)),
        //Ia^Db
        (MInt aInt, MDouble bDouble, Operation.Exponent) => new MDouble(MathF.Pow((float)aInt.Value, (float)bDouble.Value)),
        #endregion
            #endregion

            _ => throw new ArgumentException($"Can't do {A.GetType()} {Operator} {B.GetType()}")
        };

        ReassingReferences(m);

        return m;
    }

    private void ReassingReferences(MathObject m)
    {
        if (MB != null && MA != null)
        {
            MB.B = m;
            MA.A = m;
            MB.MA = MA;
            MA.MB = MB;
        }

        else if (MB == null && MA != null)
        {
            MA.A = m;
            MA.MB = null;
        }

        else if (MB != null && MA == null)
        {
            MB.B = m;
            MB.MA = null;
        }
    }

    public MathOperator(Operation Operator, MathObject a, MathObject b, int order)
    {
        this.Operator = Operator;
        A = a;
        B = b;
        Order = order;
    }
}
