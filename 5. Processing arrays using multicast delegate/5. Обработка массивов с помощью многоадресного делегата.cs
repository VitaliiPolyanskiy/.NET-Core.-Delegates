using System;

namespace TwoDimensionalArray
{
    delegate void Action(int[] a);
    class MyArray
    {
        static Random rnd = new Random();
        public static void Init(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(1, 100);
            }
        }
        public static void Print(int[] a)
        {
            foreach (int i in a)
                Console.Write("{0,5}", i);
            Console.WriteLine();
        }
        public static void Sum(int[] a)
        {
            int sum = 0;
            foreach (int i in a)
                sum += i;
            Console.WriteLine("Сумма элементов массива {0}", sum);
        }

        public static void Sort(int[] a)
        {
            Array.Sort(a);
        }

        public static void MaxElement(int[] a)
        {
            int index = 0;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] > a[index])
                    index = i;
            }
            Console.WriteLine("Максимальный элемент массива {0}", a[index]);
        }

        public static void MinElement(int[] a)
        {
            int index = 0;
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] < a[index])
                    index = i;
            }
            Console.WriteLine("Минимальный элемент массива {0}", a[index]);
        }
        public static void Main()
        {
            Action obj = new Action(Init);
            obj += Print;
            obj += Sort;
            obj += Print;
            obj += Sum;
            obj += MinElement;
            obj += MaxElement;
            int[][] a = new int[5][];
            for (int i = 0; i < 5; i++)
            {
                a[i] = new int[10];
                Console.WriteLine("\nМассив {0}", i + 1);
                obj(a[i]);
            }
        }
    }
}
