using Core.Entities;
using Core.Functions;
using Core.StepSizes;

namespace Core.Tests;

public class ConstantStepSizeStrategyTests
{
    [Fact]
    public void GetStepSize_Should_Return_Constant_Step_Size()
    {
        var strategy = new ConstantStepSizeStrategy { StepSize = 0.1 };
        
        var f = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2,
        };

        var stepSize = strategy.Handle(f, new []{ 0.0, 1.0 }, new Vector());

        Assert.Equal(0.1, stepSize);
    }

    [Fact]
    public void GetStepSize_Should_Be_Independent_Of_Current_Vector()
    {
        var strategy = new ConstantStepSizeStrategy { StepSize = 0.3 };

        var f = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2,
        };
        
        var vectors = new []
        {
            new Vector{ Values = new [] { 1.0, 2.0 }},
            new Vector{ Values = new [] { 3.0, 4.0 }},
            new Vector{ Values = new [] { 5.0, 6.0 }}
        };

        foreach (var vector in vectors)
        {
            var stepSize = strategy.Handle(f, new [] { 1.0, 2.0 }, vector);

            Assert.Equal(0.3, stepSize);
        }
    }
}
