using Core.Derivatives;
using Core.Entities;
using Core.Functions;
using Core.StepSizes;

namespace Core;

public static class Program
{
    public static void Main()
    {
        var gradients = new []
        {
            new GradientDescent
            {
                Function = new Rosenbrock2DFunction(),
                InitialCoordinates = new Vector(1, 1),
                Derivative = new FiniteDifference(),
                StepSizeStrategy = new IpsWorstStepSizeStrategy(),
                Iterations = 10000,
            },
            new GradientDescent
            {
                Function = new Rosenbrock3DFunction(),
                InitialCoordinates = new Vector(1, 1, 1),
                Derivative = new FiniteDifference(),
                StepSizeStrategy = new IpsWorstStepSizeStrategy(),
                Iterations = 10000,
            },
        };

        foreach (var gradient in gradients)
        {
            // RunAndLogOptimizer(gradient);
            RunOptimizer(gradient);

        }
    }
    
    private static void RunAndLogOptimizer(Optimizer optimizer)
    {
        var points = optimizer.WalkthroughForMinimal();

        foreach (var (i, point) in points)
        {
            Console.WriteLine($"Point {i}: {point}");
        }
    }
    
    private static void RunOptimizer(Optimizer optimizer)
    {
        var minimal = optimizer.FindLocalMinimal();

        Console.WriteLine($"{optimizer.Function} minimal: {minimal}");
    }
}