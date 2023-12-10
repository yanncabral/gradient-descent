using Core.Entities;
using Core.Derivatives;

namespace Core.Tests.Derivatives;

public class FiniteDifferenceTests
{
    [Fact]
    public void Partial_Should_Throw_For_Invalid_Variable_Index_Exceeding_Dimension()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2
        };
        
        var vector = new []{1.0, 2.0};

        Assert.Throws<ArgumentException>(() => new FiniteDifference().Partial(function, vector, 2));
    }

    [Fact]
    public void Partial_Should_Throw_For_Invalid_Variable_Index_Less_Than_Zero()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2
        };
        
        var vector = new []{1.0, 2.0};

        Assert.Throws<ArgumentException>(() => new FiniteDifference().Partial(function, vector, -1));
    }

    [Fact]
    public void Partial_Should_Calculate_Partial_Derivative_For_X()
    {
        const double stepSize = 1e-5;
        var function = new ArbitraryMathFunction
        {
            Function = x => 1 / x[0],
            Dimension = 1
        };
        
        var point = new []{ 2.0 };
        var diff = new FiniteDifference();

        var partialDerivativeX = diff.Partial(
            function, 
            point, 
            0
        );

        Assert.InRange(partialDerivativeX, -0.25 - stepSize, -0.25 + stepSize);
    }

    [Fact]
    public void Partial_Should_Calculate_Partial_Derivative_For_Y()
    {
        const double stepSize = 1e-5;
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2
        };
        
        var vector = new []{1.0, 2.0};

        var diff = new FiniteDifference();
        var partialDerivativeY = diff.Partial(function, vector, 1);

        Assert.InRange(partialDerivativeY, 1 - stepSize, 1 + stepSize);
    }

    [Fact]
    public void Partial_Should_Use_Custom_Step_Size()
    {
        var function = new ArbitraryMathFunction
        {
            Function = x => x[0] + x[1],
            Dimension = 2
        };
        
        var vector = new []{1.0, 2.0};
        var diff = new FiniteDifference();
        const double stepSize = 0.01;
        var partialDerivativeXWithCustomStep = diff.Partial(
            function: function, 
            vector: vector, 
            variableIndex: 0, 
            stepSize: stepSize
        );

        Assert.InRange(partialDerivativeXWithCustomStep, 1 - stepSize, 1 + stepSize);
    }
}