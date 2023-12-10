using Core.Entities;
using Core.StepSizes;

namespace Core;
public abstract class Optimizer
{
    public required MathFunction Function { get; init; }
    
    public abstract IEnumerable<(int, Vector)> WalkthroughForMinimal();

    public abstract OptimizationResult FindLocalMinimal();
}

public class OptimizationResult
{
    public required Vector Point { get; init; }
    public required int Iterations { get; init; }
    public required MathFunction Function { get; init; }
    public required IStepSizeStrategy Strategy { get; init; }
    public required Vector InitialCoordinates { get; init; }

    public double Value => Function.Evaluate(Point);
}