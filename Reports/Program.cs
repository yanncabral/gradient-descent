using System.Diagnostics;
using Core;
using Core.Entities;
using Core.Functions;
using Core.StepSizes;

namespace Reports;

public static class Reports
{
    public static void Main()
    {
        var p2 = Vector.Random(2);
        var p3 = Vector.Random(3);
    
        var gradients = new []
        {
            new GradientDescent
            {
                Function = new Rosenbrock2DFunction(),
                StepSizeStrategy = new ConstantStepSizeStrategy() { StepSize = 0.01 },
                InitialCoordinates = p2,
            },
            new GradientDescent
            {
                Function = new Rosenbrock2DFunction(),
                StepSizeStrategy = new IpsStepSizeStrategy(),
                InitialCoordinates = p2,
            },
            new GradientDescent
            {
                Function = new Rosenbrock2DFunction(),
                InitialCoordinates = p2,
                StepSizeStrategy = new IpsStepSizeStrategy()
                {
                    ReplacementType = IpsReplacementType.Worst,
                    Delta = 0.05,
                },
            },
            
            new GradientDescent
            {
                Function = new Rosenbrock3DFunction(),
                InitialCoordinates = p3,
                StepSizeStrategy = new ConstantStepSizeStrategy() { StepSize = 0.01 },
            },
            new GradientDescent
            {
                Function = new Rosenbrock3DFunction(),
                InitialCoordinates = p3,
                StepSizeStrategy = new IpsStepSizeStrategy(),
            },
            new GradientDescent
            {
                Function = new Rosenbrock3DFunction(),
                InitialCoordinates = p3,
                StepSizeStrategy = new IpsStepSizeStrategy()
                {
                    ReplacementType = IpsReplacementType.Worst,
                    Delta = 0.05,
                },
            },
            
            new GradientDescent
            {
                Function = new CommomFunction(),
                InitialCoordinates = p2,
                StepSizeStrategy = new ConstantStepSizeStrategy() { StepSize = 0.01 },
            },
            new GradientDescent
            {
                Function = new CommomFunction(),
                InitialCoordinates = p2,
                StepSizeStrategy = new IpsStepSizeStrategy(),
            },
            new GradientDescent
            {
                Function = new CommomFunction(),
                InitialCoordinates = p2,
                StepSizeStrategy = new IpsStepSizeStrategy()
                {
                    ReplacementType = IpsReplacementType.Worst,
                    Delta = 0.05,
                },
            },
            
        };

        GenerateReport(gradients);
    }

    public static void RunAndLogOptimizer(GradientDescent optimizer)
    {
        var points = optimizer.WalkthroughForMinimal();

        foreach (var (i, point) in points)
        {
            Console.WriteLine($"Step {i}: {point}");
        }
    }
    
    private static void GenerateReport(IEnumerable<GradientDescent> optimizers)
    {
        Console.WriteLine("Function\tStrategy\tIterations\tInitialCoordinates\tMinimalPoint\tMinimalValue\tElapsedTime");
        foreach (var optimizer in optimizers)
        {
            var watch = Stopwatch.StartNew();
            var minimal = optimizer.FindLocalMinimal();
            watch.Stop();
            var elapsed = (int) watch.Elapsed.TotalMilliseconds;

            Console.WriteLine($"{minimal.Function}\t{minimal.Strategy}\t{minimal.Iterations}\t{minimal.InitialCoordinates}\t{minimal.Point}\t{minimal.Value}\t{elapsed}ms");
        }
    }
}