using Core.Entities;

namespace Core.Functions;

public class CommomFunction : MathFunction
{
    public override int Dimension { get; init; } = 2;

    public override double Evaluate(Vector point)
    {
        var x = point.Values[0];
        var y = point.Values[1];

        var terms = new[]
        {
            Math.Pow(x, 2),
            Math.Pow(y, 2),
            2 * Math.Pow(x, 2) * Math.Pow(y, 2),
            6 * x * y,
            -4 * x,
            -4 * y,
            1
        };

        return terms.Sum();
    }
}