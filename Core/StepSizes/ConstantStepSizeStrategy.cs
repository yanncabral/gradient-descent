using Core.Entities;

namespace Core.StepSizes;

public class ConstantStepSizeStrategy : IStepSizeStrategy
{
    public required double StepSize { get; init; }

    public double Handle(MathFunction f, Vector point, Vector gradient)
    {
        return StepSize;
    }
}