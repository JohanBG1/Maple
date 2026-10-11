
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
        if(OperatorRegistry.Instance.TryGet(A.GetType(), B.GetType(), Operator, out Func<MathObject, MathObject, MathObject> operation))
        {
            MathObject m = operation?.Invoke(A, B);
            ReassingReferences(m);
            return m;
        }

        throw new ArgumentException($"can't {A.GetType()} {OperationHelper.ConvertFromOperationToString(Operator)} {B.GetType()}");
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
