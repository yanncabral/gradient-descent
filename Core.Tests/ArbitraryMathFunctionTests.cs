using Core.Entities;
using Core.Functions;

namespace Core.Tests;

public class ArbitraryMathFunctionTests
{
    [Fact]
    public void Dimension_Should_Be_Initialized_By_Function_Arity()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2,
        };

        Assert.Equal(2, function.Dimension);
    }

    [Fact]
    public void Calculate_Should_Throw_For_Incompatible_Dimension()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2,
        };
        
        var point = new Vector
        {
            Values = new [] { 1.0 },
        };

        Assert.Throws<ArgumentException>(() => function.Evaluate(point));
    }

    [Fact]
    public void Calculate_Should_Return_Function_Value()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2,
        };
        
        var result = function.Evaluate(new [] { 1.0, 2.0 });

        Assert.Equal(3.0, result);
    }
}
