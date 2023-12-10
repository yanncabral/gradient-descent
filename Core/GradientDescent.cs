using Core.Entities;
using Core.Derivatives;
using Core.StepSizes;

namespace Core;

public class GradientDescent : Optimizer
{
    public IDerivative Derivative { get; init; } = new FiniteDifference();
    public int MaxIterations { get; init; } = 1000000;
    public Vector? InitialCoordinates { get; set; }
    public double Epsilon { get; init; } = 1e-7;
    public required IStepSizeStrategy StepSizeStrategy { get; init; }

    public override IEnumerable<(int, Vector)> WalkthroughForMinimal()
    {
        var currentPoint = InitialCoordinates ?? Vector.Random(Function.Dimension);

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

    public override OptimizationResult FindLocalMinimal()
    {
        InitialCoordinates ??= Vector.Random(Function.Dimension);
        var walkthrough = WalkthroughForMinimal();
        
        var lastStep = walkthrough.Last();

        return new OptimizationResult()
        {
            Point = lastStep.Item2,
            Function = Function,
            Iterations = lastStep.Item1,
            InitialCoordinates = InitialCoordinates,
            Strategy = StepSizeStrategy,
        };
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
    
    public override string ToString()
    {
        return $"{Function}({StepSizeStrategy})";
    }
}