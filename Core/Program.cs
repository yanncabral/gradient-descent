using System.Diagnostics;
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
                InitialCoordinates = Vector.Random(2),
                StepSizeStrategy = new IpsStepSizeStrategy(),
            },
            new GradientDescent
            {
                Function = new Rosenbrock3DFunction(),
                InitialCoordinates = Vector.Random(3),
                StepSizeStrategy = new IpsStepSizeStrategy()
                {
                    ReplacementType = IpsReplacementType.Worst
                },
            },
        };

        foreach (var gradient in gradients)
        {
            RunOptimizer(gradient);

        }
        
        // RunAndLogOptimizer(new GradientDescent
        // {
        //     Function = new Rosenbrock3DFunction(),
        //     InitialCoordinates = Vector.Random(3, min: -10000, max: 10000),
        //     Derivative = new FiniteDifference(),
        //     // StepSizeStrategy = new ConstantStepSizeStrategy() { StepSize = 0.01 },
        //     StepSizeStrategy = new IpsStepSizeStrategy
        //     {
        //         ReplacementType = IpsReplacementType.Ciclical,
        //     },
        //     MaxIterations = 1000000,
        // });
    }
    
    private static void RunAndLogOptimizer(GradientDescent optimizer)
    {
        var points = optimizer.WalkthroughForMinimal();

        foreach (var (i, point) in points)
        {
            Console.WriteLine($"Point {i}: {point}");
        }
    }
    
    private static void RunOptimizer(GradientDescent optimizer)
    {
        var watch = Stopwatch.StartNew();
        var minimal = optimizer.FindLocalMinimal();
        watch.Stop();
        var elapsed = watch.Elapsed.Milliseconds;

        Console.WriteLine($"{optimizer.Function}({optimizer.StepSizeStrategy}): {minimal} in {elapsed}ms.");
    }
}