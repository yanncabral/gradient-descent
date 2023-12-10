using Core.Entities;

namespace Core.Derivatives;

public interface IDerivative
{
    double Partial(MathFunction function, Vector vector, int variableIndex);
}