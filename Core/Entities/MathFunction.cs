namespace Core.Entities;

public abstract class MathFunction
{
    public abstract int Dimension { get; init; }

    public abstract double Evaluate(Vector point);

    public override string ToString()
    {
        return base.ToString()?.Split('.').Last() ?? "Unknown";
    }
}