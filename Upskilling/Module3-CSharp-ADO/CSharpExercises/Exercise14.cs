using System;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 14: Demonstrate Inheritance and Method Overriding
    /// </summary>
    public static class Exercise14
    {
        public class Shape
        {
            public virtual void Draw()
            {
                Console.WriteLine("Drawing a generic shape.");
            }
        }

        public class Circle : Shape
        {
            public double Radius { get; set; }

            public Circle(double radius) => Radius = radius;

            public override void Draw()
            {
                Console.WriteLine($"Drawing a Circle with radius {Radius} (area = {Math.PI * Radius * Radius:F2}).");
            }
        }

        public class Rectangle : Shape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public override void Draw()
            {
                Console.WriteLine($"Drawing a Rectangle {Width}x{Height} (area = {Width * Height:F2}).");
            }
        }

        public static void Run()
        {
            Shape genericShape = new Shape();
            Shape circle = new Circle(5);
            Shape rectangle = new Rectangle(4, 6);

            genericShape.Draw();
            circle.Draw();
            rectangle.Draw();
        }
    }
}
