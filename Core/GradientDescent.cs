using Core.Entities;
using Core.Derivatives;
using Core.StepSizes;

namespace Core;

public class GradientDescent : Optimizer
{
    public IDerivative Derivative { get; init; } = new FiniteDifference();
    public int Iterations { get; init; } = 10000;
    public Vector InitialCoordinates { get; init; } = new (0, 0);
    public double Epsilon { get; init; } = 1e-6;
    public required IStepSizeStrategy StepSizeStrategy { get; init; }

    public override IEnumerable<(int, Vector)> WalkthroughForMinimal()
    {
        var currentPoint = InitialCoordinates;

        for (var i = 0; i < Iterations; i++)
        {
            yield return (i, currentPoint);
            var gradient = CalculateGradient(currentPoint);
            
            if (gradient.Norm < Epsilon)
            {
                yield break;
            }

            var stepSize = StepSizeStrategy.Handle(
                f: Function,
                point: currentPoint,
                gradient: gradient
            );

            currentPoint -= currentPoint * stepSize;
        }
    }

    public override Vector FindLocalMinimal()
    {
        return WalkthroughForMinimal().Last().Item2;
    }

    private Vector CalculateGradient(Vector vector)
    {
        var derivatives = new double[Function.Dimension];

        for (var i = 0; i < Function.Dimension; i++)
        {
            derivatives[i] = Derivative.Partial(Function, vector, i);
        }

        return new Vector(derivatives).Normalized();
    }
}