using Core.Entities;

namespace Core.StepSizes;

public class IpsCiclicalStepSizeStrategy : IStepSizeStrategy
{
    public double Target { get; init; } = 1e-6;
    public double Alpha { get; init; } = 0.01;
    public double Delta { get; init; } = 0.001;
    public int MaxIterations { get; init; } = 100;

    public double Handle(MathFunction f, Vector point, Vector gradient)
    {
        if (Delta <= 0) throw new ArgumentException("Delta must be positive.");
        if (MaxIterations <= 0) throw new ArgumentException("MaxIterations must be positive.");

        var estimates = new List<double> { Alpha + Delta, Alpha, Alpha - Delta };
        var u = Alpha;

        for (var i = 0; i < MaxIterations; i++)
        {
            var fr = f.Evaluate(point - gradient * estimates[0]);
            var fs = f.Evaluate(point - gradient * estimates[1]);
            var ft = f.Evaluate(point - gradient * estimates[2]);

            var numerator = (fs - fr) * (estimates[2] - estimates[0]) * (estimates[2] - estimates[1]);
            var denominator = ((estimates[1] - estimates[0]) * (ft - fs) - (fs - fr) * (estimates[2] - estimates[1]));

            if (Math.Abs(denominator) < 1e-10)
            {
                // Handle small or zero denominator
                u = (estimates[0] + estimates[1] + estimates[2]) / 3;
            }
            else
            {
                u = ((estimates[0] + estimates[1]) - (numerator / denominator) / 2 );
            }

            // Update estimates
            estimates.RemoveAt(0);
            estimates.Add(u);

            if (Math.Abs(u) <= Target)
            {
                break;
            }
        }

        return u;
    }
}
