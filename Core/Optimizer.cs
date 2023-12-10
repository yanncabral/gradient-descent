using Core.Entities;

namespace Core;
public abstract class Optimizer
{
    public required MathFunction Function { get; init; }
    
    public abstract IEnumerable<(int, Vector)> WalkthroughForMinimal();

    public abstract Vector FindLocalMinimal();
}