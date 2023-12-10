namespace Core.Entities;

public class Vector
{
    public double[] Values { get; init; } = Array.Empty<double>();
    
    public double this[int index]
    {
        get => Values[index];
        set => Values[index] = value;
    }

    public static implicit operator Vector(double[] values) => new Vector(values);
    
    public double Norm => Math.Sqrt(Values.Sum(x => Math.Pow(x, 2)));
    
    public Vector Clone()
    {
        return new Vector
        {
            Values = (Values.Clone() as double[])!
        };
    }

    public int Dimension => Values.Length;

    public Vector() {}

    public Vector(params double[] values) => Values = values;

    public static Vector Random(int dimension, double min = -100, double max = 100)
    {
        var random = new Random();
        var values = new double[dimension];

        for (var i = 0; i < dimension; i++)
        {
            values[i] = random.NextDouble() * (max - min) + min;
        }

        return new Vector(values);
    }

    public override string ToString()
    {
        return $"({string.Join(", ", Values)})";
    }
    
    public static Vector operator +(Vector a, Vector b)
    {
        var result = new double[a.Dimension];
    
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] + b[i];
        }
    
        return new Vector(result);
    }
    
    public static Vector operator -(Vector a, Vector b)
    {
        var result = new double[a.Dimension];
    
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] - b[i];
        }
    
        return new Vector(result);
    }
    
    public static Vector operator *(Vector a, double b)
    {
        var result = new double[a.Dimension];
        
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] * b;
        }
    
        return result;
    }
    
    public static Vector operator *(double a, Vector b)
    {
        return b * a;
    }
    
    public static Vector operator *(Vector a, Vector b)
    {
        var result = new double[a.Dimension];
    
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] * b[i];
        }
    
        return new Vector(result);
    }
    
    public static Vector operator /(Vector a, double b)
    {
        var result = new double[a.Dimension];
    
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] / b;
        }
    
        return new Vector(result);
    }
    
    public static Vector operator /(Vector a, Vector b)
    {
        var result = new double[a.Dimension];
    
        for (var i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] / b[i];
        }
    
        return new Vector(result);
    }

    public Vector Normalized() => this / Norm;
    
    public double X => this[0];
    public double Y => this[1];
    public double Z => this[2];
}