using SomeCompanyTask2022.Lib.Utils;

namespace SomeCompanyTask2022.Lib.Shapes;

public class Circle : ICalculableArea
{
    public readonly double Radius;

    /// <exception cref="ArgumentException">If radius less then zero</exception>
    public Circle(double radius)
    {
        if (radius <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(radius));

        Radius = radius;
    }
    
    
    /// <summary>
    /// Calculates area of a circle
    /// </summary>
    public double CalculateArea()
    {
        return Math.PI * Math.Pow(Radius, 2);
    }
}