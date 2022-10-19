using SomeCompanyTask2022.Lib.Utils;

namespace SomeCompanyTask2022.Lib.Shapes;

public class Triangle : ICalculableArea
{
    public double A { get; }
    public double B { get; }
    public double C { get; }
    
    /// <exception cref="ArgumentException">If any side is negative or zero</exception>
    public Triangle(double edgeA, double edgeB, double edgeC)
    {
        if (edgeA <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(edgeA));
        if (edgeB <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(edgeB));
        if (edgeC <= 0) throw new ArgumentException(Exceptions.InvalidLengthNumberExceptionMessage, nameof(edgeC));
        if (!AreSidesValidLengths(edgeA, edgeB, edgeC))
            throw new ArgumentException($"The sum of the lengths of any two sides of a triangle must be greater than the length of the third side");

        A = edgeA;
        B = edgeB;
        C = edgeC;
    }

    /// <summary>
    /// Calculates area of triangle from 3 sides' lengths
    /// </summary>
    public double CalculateArea()
    {
        var semiPerimeter = (A + B + C) / 2;

        return Math.Sqrt(semiPerimeter * (semiPerimeter - A) * (semiPerimeter - B) * (semiPerimeter - C));
    }
    
    public bool IsRightTriangle()
    {
        var arr = new[] { A, B, C };

        HandlePrecisionIssues(arr);
        
        // Biggest side is arr[2] now
        Array.Sort(arr);

        // a^2 + b^2 = c^2
        // a^2 + b^2 - c^2 < precision
        /**
         *   ^
         *   |\
         *   | \
         * a |  \ c
         *   |   \
         *   |____\
         *     b
         */
        return Math.Abs(Math.Pow(arr[0], 2) + Math.Pow(arr[1], 2) - Math.Pow(arr[2], 2)) < LibMath.Precision;
    }

    private static bool AreSidesValidLengths(double a, double b, double c)
    {
        return (a + b > c) && (a + c > b) && (b + c > a);
    }

    /// <summary>
    /// Trying to handle precision issues
    /// For example without further "while"
    /// ▲ with sides 1 1 √2 would be fine and return true
    /// ▲ with sides 1e6 1e6 (1e6 * √2) would return false
    /// same with sides too small
    /// </summary>
    private static void HandlePrecisionIssues(IList<double> arr)
    {
        // Are all bigger then
        while (arr.All(v => v >= 1_000))
        {
            arr[0] /= 1000;
            arr[1] /= 1000;
            arr[2] /= 1000;
        }
        
        // Are all smaller then
        while (arr.All(v => v <= 0.000_001))
        {
            arr[0] *= 1000;
            arr[1] *= 1000;
            arr[2] *= 1000;
        }
    }
}