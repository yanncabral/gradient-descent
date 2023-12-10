using Core.Entities;

namespace Core.StepSizes;

public class IpsWorstStepSizeStrategy : IStepSizeStrategy
{
    public double Target { get; init; }  = 1e-6;
    public double Alpha { get; init; } = 0.01;
    public double Delta { get; init; } = 0.001;
    public double MaxIterations { get; init; } = 100;
    
    public double Handle(MathFunction f, Vector point, Vector gradient)
    {
        var rst = new[] {Alpha + Delta, Alpha, Alpha - Delta};
        for (var i = 0; i < MaxIterations; i++)
        {
            
            var fr = f.Evaluate(point - gradient * rst[0]);
            var fs = f.Evaluate(point - gradient * rst[1]);
            var ft = f.Evaluate(point - gradient * rst[2]);
        
            
            var x = (fs - fr) * (rst[2] - rst[0]) * (rst[2] - rst[1]);
            var y = 2 * ((rst[1] - rst[0]) * (ft - fs) - (fs - fr) * (rst[2] - rst[1]));

            var u = ((rst[0] + rst[1]) / 2) - (x / y);
            

            rst[Array.IndexOf(rst, rst.Max())] = u;
            
            if (u <= 1e-6)
            {
                break;
            }
        }
        
        return rst.Min();
    }
}