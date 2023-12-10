using Core.Entities;
using Core.StepSize;

namespace Core.Tests;

public class ConstantStepSizeStrategyTests
{
    [Fact]
    public void GetStepSize_Should_Return_Constant_Step_Size()
    {
        var strategy = new ConstantStepSizeStrategy { StepSize = 0.1 };

        var stepSize = strategy.Handle(1, new Vector());

        Assert.Equal(0.1, stepSize);
    }

    [Fact]
    public void GetStepSize_Should_Be_Independent_Of_Iteration()
    {
        var strategy = new ConstantStepSizeStrategy { StepSize = 0.2 };

        for (var i = 0; i < 10; i++)
        {
            var stepSize = strategy.Handle(i, new Vector());

            Assert.Equal(0.2, stepSize);
        }
    }

    [Fact]
    public void GetStepSize_Should_Be_Independent_Of_Current_Vector()
    {
        var strategy = new ConstantStepSizeStrategy { StepSize = 0.3 };

        var vectors = new []
        {
            new Vector{ Values = new [] { 1.0, 2.0 }},
            new Vector{ Values = new [] { 3.0, 4.0 }},
            new Vector{ Values = new [] { 5.0, 6.0 }}
        };

        foreach (var vector in vectors)
        {
            var stepSize = strategy.Handle(1, vector);

            Assert.Equal(0.3, stepSize);
        }
    }
}
