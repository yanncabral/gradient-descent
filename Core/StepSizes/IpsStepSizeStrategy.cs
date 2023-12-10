using Core.Entities;

namespace Core.StepSizes;

public enum IpsReplacementType
{
    /// <summary>
    /// Substitui o ponto mais antigo pelo recém calculado.
    /// </summary>
    Ciclical,
    /// <summary>
    /// Substitui a pior estimativa pelo recém calculado.
    /// </summary>
    Worst,
}

public class IpsStepSizeStrategy : IStepSizeStrategy
{
    public double Epsilon { get; init; } = 1e-7;
    public int MaxIterations { get; init; } = 100;
    public double Alpha { get; init; } = 0.01;
    public double Delta { get; init; } = 0.001;
    public IpsReplacementType ReplacementType { get; init; } = IpsReplacementType.Ciclical;
    

    public double Handle(MathFunction f, Vector point, Vector gradient)
    {
        if (Delta <= 0) throw new ArgumentException("Delta must be positive.");
        if (MaxIterations <= 0) throw new ArgumentException("MaxIterations must be positive.");

        var estimates = new List<double> { Alpha + Delta, Alpha, Alpha - Delta };
        var stepSize = Alpha;

        for (var i = 0; i < MaxIterations; i++)
        {
            var fr = f.Evaluate(point - gradient * estimates[0]);
            var fs = f.Evaluate(point - gradient * estimates[1]);
            var ft = f.Evaluate(point - gradient * estimates[2]);

            var numerator = (fs - fr) * (estimates[2] - estimates[0]) * (estimates[2] - estimates[1]);
            var denominator = 2 * ((estimates[1] - estimates[0]) * (ft - fs) - (fs - fr) * (estimates[2] - estimates[1]));

            if (Math.Abs(denominator) < 1e-10)
            {
                // Handle small or zero denominator
                stepSize = estimates.Sum() / 3;
            }
            else
            {
                stepSize = ((estimates[0] + estimates[1]) / 2 - (numerator / denominator));
            }

            if (Math.Abs(estimates[0] - stepSize) < Epsilon)
            {
                break;
            }
            
            estimates.Add(stepSize);

            switch (ReplacementType)
            {
                case IpsReplacementType.Ciclical:
                    // Update estimates
                    estimates.RemoveAt(0);
                    break;
                case IpsReplacementType.Worst:
                    estimates.Sort();
                    estimates.RemoveAt(estimates.Count - 1);
                    break;
            }
        }

        return stepSize;
    }
}