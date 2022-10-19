using FluentAssertions;
using SomeCompanyTask2022.Lib.Shapes;

namespace SomeCompanyTask2022.Lib.Tests;

public class CircleTests
{
    public record CircleData(double Radius, double Area);
    
    public static IEnumerable<object[]> ValidCircleData = new CircleData[][]
    {
        new [] { new CircleData(1, Math.PI) },
        new [] { new CircleData(2, 4 * Math.PI) },
        new [] { new CircleData(1e6, 1e12 * Math.PI) },
        new [] { new CircleData(0.1, 1e-2 * Math.PI) },
        new [] { new CircleData(1e-6, 1e-12 * Math.PI) },
    };
    
    [Theory]
    [MemberData(nameof(ValidCircleData))]
    public void Ctor_ShouldNotThrow_WhenInputIsValid(CircleData data)
    {
        var (radius, _) = data;
        var act = () => new Circle(radius);

        act
            .Should()
            .NotThrow();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-1_000_000)]
    [InlineData(-0.5)]
    [InlineData(-0.000_001)]
    public void Ctor_ShouldThrow_WhenInputIsInvalid(double radius)
    {
        var act = () => new Circle(radius);

        act
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Theory]
    [MemberData(nameof(ValidCircleData))]
    public void CalculateArea_ShouldCalculateCorrectly(CircleData data)
    {
        var (radius, expected) = data;
        
        var result = new Circle(radius).CalculateArea();

        result
            .Should()
            .Be(expected);
    }
}