using Core.Entities;

namespace Core.Derivatives;

public class FiniteDifference : IDerivative
{
    public double Partial(MathFunction function, Vector vector, int variableIndex, double stepSize = 1e-5)
    {
        if (vector.Dimension <= variableIndex || variableIndex < 0)
        {
            throw new ArgumentException(
                $"The variable index ({variableIndex}) is greater than or equal to the dimension of the point ({vector.Dimension})."
            );
        }

        var pointWithDelta = vector.Clone();
        pointWithDelta[variableIndex] += stepSize;

        // f'(x) = (f(x + h) - f(x)) / h
        return (function.Evaluate(pointWithDelta) - function.Evaluate(vector)) / stepSize;
    }
}