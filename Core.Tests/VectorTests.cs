using Core.Entities;

namespace Core.Tests;

public class VectorTests
{
    [Fact]
    public void Values_Should_Be_Initialized_To_Empty()
    {
        var vector = new Vector();

        Assert.Empty(vector.Values);
    }

    [Fact]
    public void Values_Should_Be_Settable()
    {
        var vector = new Vector(1.0, 2.0);

        Assert.Equal(1.0, vector.Values[0]);
        Assert.Equal(2.0, vector.Values[1]);
    }

    [Fact]
    public void Norm_Should_Return_The_Euclidean_Norm()
    {
        var vector = new Vector() { Values = new [] { 1.0, 2.0 } };

        Assert.Equal(vector.Norm, Math.Sqrt(5.0));
    }

    [Fact]
    public void Clone_Should_Return_A_Deep_Copy()
    {
        var vector = new Vector() { Values = new [] { 1.0, 2.0 } };

        var clone = vector.Clone();

        Assert.Equal(vector.Values, clone.Values);

        clone[0] = 3.0;

        Assert.NotEqual(vector[0], clone[0]);
    }

    [Fact]
    public void Dimension_Should_Return_The_Dimension()
    {
        var vector = new Vector() { Values = new [] { 1.0, 2.0 } };
        
        Assert.Equal(2, vector.Dimension);
    }

    [Fact]
    public void ToString_Should_Return_A_String_Representation_Of_The_Vector()
    {
        var vector = new Vector() { Values = new [] { 1.0, 2.0 } };

        Assert.Equal("(1, 2)", vector.ToString());
    }
    
    [Fact]
    public void ToString_Should_Return_A_String_Representation_Of_The_Vector_When_Empty()
    {
        var vector = new Vector();

        Assert.Equal("()", vector.ToString());
    }
}
