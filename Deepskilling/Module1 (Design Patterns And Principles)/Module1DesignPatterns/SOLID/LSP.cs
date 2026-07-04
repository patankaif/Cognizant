namespace Module1DesignPatterns.SOLID.LSP
{
    namespace Bad
    {
        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public int GetArea() => Width * Height;
        }

        public class Square : Rectangle
        {
            public override int Width
            {
                get => base.Width;
                set { base.Width = value; base.Height = value; } 
            }

            public override int Height
            {
                get => base.Height;
                set { base.Width = value; base.Height = value; } 
            }
        }

        public static class Client
        {
            public static void Resize(Rectangle rectangle)
            {
                rectangle.Width = 5;
                rectangle.Height = 10;
                Console.WriteLine($"Expected area 50, got {rectangle.GetArea()}");
            }
        }
    }

    namespace Good
    {
        public interface IShape
        {
            int GetArea();
        }

        public class Rectangle : IShape
        {
            public int Width { get; }
            public int Height { get; }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int GetArea() => Width * Height;
        }

        public class Square : IShape
        {
            public int Side { get; }

            public Square(int side)
            {
                Side = side;
            }

            public int GetArea() => Side * Side;
        }

        public static class Client
        {
            public static void PrintArea(IShape shape)
            {
                Console.WriteLine($"Area: {shape.GetArea()}");
            }
        }
    }

    public static class LspDemo
    {
        public static void Run()
        {
            Console.WriteLine("--- LSP: Bad (Square breaks Rectangle's contract) ---");
            Bad.Client.Resize(new Bad.Rectangle());
            Bad.Client.Resize(new Bad.Square()); 

            Console.WriteLine();
            Console.WriteLine("--- LSP: Good (both shapes honor the same honest contract) ---");
            Good.Client.PrintArea(new Good.Rectangle(5, 10));
            Good.Client.PrintArea(new Good.Square(5));
        }
    }
}
