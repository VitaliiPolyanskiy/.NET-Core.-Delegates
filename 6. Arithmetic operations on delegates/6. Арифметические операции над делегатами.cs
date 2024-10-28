using System;
namespace Delegate
{
    class Program
    {
        private class Circle
        {
            public double Radius;

            public Circle(double radius)
            {
                this.Radius = radius;
            }

            public void PrintRadius()
            {
                Console.WriteLine("Радиус равен :           {0,7:N3}", Radius);
            }

            public void PrintDiameter()
            {
                Double diameter = 2 * Radius;
                Console.WriteLine("Диаметр равен :          {0,7:N3}", diameter);
            }

            public void PrintLength()
            {
                double length = 2 * Math.PI * Radius;
                Console.WriteLine("Длина окружности равна : {0,7:N3}", length);
            }

            public void PrintSquare()
            {
                double square = Math.PI * Radius * Radius;
                Console.WriteLine("Площадь круга равна :    {0,7:N3}", square);
            }
        }

        private delegate void PrintInfo();

        static void Main(string[] args)
        {
            Circle circle = new Circle(4);

            PrintInfo printInfo = new PrintInfo(circle.PrintRadius);
            foreach (PrintInfo item in printInfo.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }
            Console.WriteLine("\nДелегат инициализирован одним методом" +
                " PrintRadius()");            
            printInfo();

            printInfo += circle.PrintDiameter;
            printInfo += circle.PrintLength;
            printInfo += circle.PrintSquare;
            foreach (PrintInfo item in printInfo.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }
            Console.WriteLine("\nДелегат вызывает цепочку методов \nPrintRadius()," +
                " PrintDiameter(), PrintLength(), PrintSquare() :\n");
            printInfo();
            Console.WriteLine();
            PrintInfo printInfo1 = circle.PrintSquare;
            foreach (PrintInfo item in printInfo1.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }
            printInfo -= printInfo1;
            Console.WriteLine();
            foreach (PrintInfo item in printInfo.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }
            Console.WriteLine(
                "\nТеперь делегат вызывает методы \nPrintRadius()," +
                " PrintDiameter(), PrintLength() :\n");
            printInfo();


            printInfo = printInfo + printInfo1 - new PrintInfo(circle.PrintRadius)
                - new PrintInfo(circle.PrintDiameter);
            foreach (PrintInfo item in printInfo.GetInvocationList())
            {
                Console.WriteLine("{0}", item.Method.Name);
            }
            Console.WriteLine("\nТеперь делегат вызывает методы" +
                " \nPrintLength(), PrintSquare() :\n");
            printInfo();

            Console.WriteLine("\n");
        }
    }
}
 