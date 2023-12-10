using Core.Entities;

namespace Core.StepSizes;

public interface IStepSizeStrategy
{
    public double Handle(MathFunction f, Vector point, Vector gradient);
}