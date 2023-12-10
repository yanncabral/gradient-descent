using Core.Entities;
using Core.Derivatives;
using Core.StepSizes;

namespace Core;

public class GradientDescent : Optimizer
{
    public IDerivative Derivative { get; init; } = new FiniteDifference();
    public int MaxIterations { get; init; } = 1000000;
    public Vector? InitialCoordinates { get; init; }
    public double Epsilon { get; init; } = double.Epsilon;
    public required IStepSizeStrategy StepSizeStrategy { get; init; }

    public override IEnumerable<(int, Vector)> WalkthroughForMinimal()
    {
        var currentPoint = InitialCoordinates;

        for (var i = 0; i < MaxIterations; i++)
        {
            yield return (i, currentPoint);
            var gradient = CalculateGradient(currentPoint);
            
            var stepSize = StepSizeStrategy.Handle(
                f: Function,
                point: currentPoint,
                gradient: gradient
            );

            if (stepSize < Epsilon)
            {
                yield break;
            }

            currentPoint -= gradient * stepSize;
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