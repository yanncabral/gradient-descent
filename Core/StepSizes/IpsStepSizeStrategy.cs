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
    public double Alpha { get; init; } = 0.1;
    public double Delta { get; init; } = 0.005;
    public IpsReplacementType ReplacementType { get; init; } = IpsReplacementType.Ciclical;

    public double Handle(MathFunction f, Vector point, Vector gradient)
    {
        if (Delta <= 0) throw new ArgumentException("Delta must be positive.");
        if (MaxIterations <= 0) throw new ArgumentException("MaxIterations must be positive.");

        var estimates = new List<double> { Alpha + Delta, Alpha, Alpha - Delta };

        for (var i = 0; i < MaxIterations; i++)
        {
            var fr = F(estimates[0], f, point, gradient);
            var fs = F(estimates[1], f, point, gradient);
            var ft = F(estimates[2], f, point, gradient);

            var numerator = (fs - fr) * (estimates[2] - estimates[0]) * (estimates[2] - estimates[1]);
            var denominator = 2 * ((estimates[1] - estimates[0]) * (ft - fs) - (fs - fr) * (estimates[2] - estimates[1]));
            
            double estimate;

            if (Math.Abs(denominator) < 1e-10)
            {
                // Handle small or zero denominator
                estimate = estimates.Sum() / 3;
            }
            else
            {
                estimate = ((estimates[0] + estimates[1]) / 2 - (numerator / denominator));
            }

            if (Math.Abs(estimates[0] - estimate) < Epsilon)
            {
                break;
            }
            
            

            UpdateEstimates(ref estimates, estimate);
        }

        return estimates.Min();
    }
    
    private void UpdateEstimates(ref List<double> estimates, double estimate)
    {
        estimates.Add(estimate);
        
        switch (ReplacementType)
        {
            case IpsReplacementType.Ciclical:
                // Update estimates
                estimates.RemoveAt(0);
                break;
            case IpsReplacementType.Worst:
            {
                var worstIndex = 0;
                for (var j = 1; j < estimates.Count; j++)
                {
                    if (estimates[j] > estimates[worstIndex])
                    {
                        worstIndex = j;
                    }
                }
                estimates.RemoveAt(worstIndex);
                break;
            }
        }
    }
    
    private static double F(double estimate, MathFunction f, Vector point, Vector gradient)
    {
        return f.Evaluate(point - gradient * estimate);
    }

    public override string ToString()
    {
        return $"{base.ToString()}({ReplacementType})";
    }
}