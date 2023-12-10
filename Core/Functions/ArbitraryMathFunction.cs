using Core.Entities;

namespace Core.Functions;

public class ArbitraryMathFunction : MathFunction
{
    public override int Dimension { get; init; }
    public required Func<Vector, double> Function { get; init; }

    public override double Evaluate(Vector point)
    {
        if (point.Dimension != Dimension)
        {
            throw new ArgumentException(
                $"The dimension of the point ({point.Dimension}) is different from the dimension of the function ({Dimension})."
            );
        }
            

        return Function(point);
    }
}