using FluentAssertions;
using SomeCompanyTask2022.Lib.Shapes;

namespace SomeCompanyTask2022.Lib.Tests;

public class TriangleTests
{
    public record TriangleData(double A, double B, double C, double AreaOfTriangle, bool IsRightTriangle);
    
    public static IEnumerable<object[]> ValidTriangleData = new TriangleData[][]
    {
        new [] {new TriangleData(1, 1, 1, 0.4330127018922193, false)},
        new [] {new TriangleData(0.1, 0.1, 0.1, 0.004330127018922195, false)},
        new [] {new TriangleData(5, 7, 10, 16.24807680927192, false)},
        new [] {new TriangleData(3.0, 4, 5, 6, true)},
        new [] {new TriangleData(1, 1, Math.Sqrt(2), 0.49999999999999983, true)},
        new [] {new TriangleData(1e6, 1e6, Math.Sqrt(2), 707106.781191947, false)},
        new [] {new TriangleData(3, 1_000_001, 1_000_000, 1414214.2694782852, false)},
        new [] {new TriangleData(1e6, 1e6, 1e6 * Math.Sqrt(2), 499999999999.99994, true)},
        new [] {new TriangleData(1e12, 1e12, 1e12 * Math.Sqrt(2), 5E+23, true)},
        new [] {new TriangleData(1e-12, 1e-12, 1e-12 * Math.Sqrt(2), 5E-25, true)},
        new [] {new TriangleData(4, 4, 5, 7.806247497997997,  false)},
        new [] {new TriangleData(0.3, 0.4, 0.5, 0.059999999999999984, true)},
    };
    
    [Theory]
    [MemberData(nameof(ValidTriangleData))]
    public void Ctor_ShouldNotThrow_WhenInputIsValid(TriangleData data)
    {
        var (a, b, c, _, _) = data;
        
        var act = () => new Triangle(a, b, c);

        act
            .Should()
            .NotThrow();
    }
    
    [Theory]
    [InlineData(1, 2, 0)]
    [InlineData(1, 2, -3)]
    [InlineData(1, 0, 3)]
    [InlineData(1, -1, 0)]
    [InlineData(-1, -1, -1)]
    [InlineData(1, 2, 3)]
    [InlineData(1_000_000, 1, 1)]
    [InlineData(0.1, 0.2, 3)]
    [InlineData(0.000_001, 1_000_000, 1)]
    public void Ctor_ShouldThrow_WhenInputIsInvalid(double a, double b, double c)
    {
        var act = () => new Triangle(a, b, c);

        act
            .Should()
            .Throw<ArgumentException>();
    }
    
    [Theory]
    [MemberData(nameof(ValidTriangleData))]
    public void CalculateArea_ShouldCalculateCorrectly(TriangleData data)
    {
        var (a, b, c, expected, _) = data;
        var result = new Triangle(a, b, c).CalculateArea();

        result
            .Should()
            .Be(expected);
    }

    [Theory]
    [MemberData(nameof(ValidTriangleData))]
    public void IsRightTriangle_ShouldDetectRightTriangleCorrectly(TriangleData data)
    {
        var (a, b, c, _, expected) = data;
        var result = new Triangle(a, b, c).IsRightTriangle();

        result
            .Should()
            .Be(expected);
    }
}