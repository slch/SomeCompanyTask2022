using SomeCompanyTask2022.Lib.Shapes;
using SomeCompanyTask2022.Lib.Utils;
// ReSharper disable CommentTypo

// Write a lib in C# for delivering to 3rd parties
// It should calculate area of a circle with radius and area of triangle by 3 sides
// ✔️
var triangleInstance = new Triangle(1, 1, 1);
var circleInstance = new Circle(1);

Console.WriteLine($"Triangle area: {triangleInstance.CalculateArea()}");
Console.WriteLine($"Circle area: {circleInstance.CalculateArea()}");

Console.WriteLine();

// Additionally will assess:
// Unit-tests
// ✔️
// See "SomeCompanyTask2022.Lib.Tests" project

// Calculating area of a shape without knowing it's type in compile time
// ✔
// ICalculableArea.CalculateArea();

var unknownShapes = new object[] { triangleInstance, circleInstance, new Line(1), new Square(5) };

foreach (var shape in unknownShapes)
{
    var shapeWithAddedAreaMethod = shape as ICalculableArea;
    
    Console.WriteLine($"Type: {shape.GetType()}, Area: {(shapeWithAddedAreaMethod is null ? "Unknown" : shapeWithAddedAreaMethod.CalculateArea())}");
}

Console.WriteLine();

// Right triangle check
// ✔
new List<Triangle>
{
    triangleInstance,
    new (1, 1, Math.Sqrt(2)),
}.ForEach(t =>
{
    Console.WriteLine($"Triangle is: {(t.IsRightTriangle() ? "Right" : "Not Right")}");
});

Console.WriteLine();

// Ease of new figure type creation
// ✔️
public class Line
{
    public readonly double Length;
    
    public Line(double length)
    {
        if (length <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(length));
        
        Length = length;
    }
}

public class Square : ICalculableArea
{
    public readonly double A;
    
    public Square(double sideLength)
    {
        if (sideLength <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(sideLength));
        
        A = sideLength;
    }
    
    public double CalculateArea()
    {
        return A * A;
    }
}