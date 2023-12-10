using Core.Entities;

namespace Core.Functions;

public class Rosenbrock2DFunction : MathFunction
{
    public override int Dimension { get; init; } = 2;

    public override double Evaluate(Vector point)
    {
        var x = point.Values[0];
        var y = point.Values[1];

        var terms = new[]
        {
            100 * Math.Pow(y - Math.Pow(x, 2), 2),
            Math.Pow(x - 1, 2)
        };

        return terms.Sum();
    }
}