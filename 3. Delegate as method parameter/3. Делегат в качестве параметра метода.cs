using System;
namespace Delegate
{
    class Program
    {
        public delegate bool Comparer(int elem1, int elem2);

        static class BubbleSorter
        {
            static public void Sort(int[] array, Comparer comparer)
            {
                for (int i = 0; i < array.Length; i++)
                    for (int j = i + 1; j < array.Length; j++)
                        if (comparer(array[j], array[i]))
                        {
                            int buffer = array[i];
                            array[i] = array[j];
                            array[j] = buffer;
                        }
            }
        }

        static public bool Compare(int a, int b)
        {
            return a < b ? true : false;
        }

        static public bool Compare_descending(int a, int b)
        {
            return a > b ? true : false;
        }

        static public bool Compare_negative_positive(int a, int b)
        {
            return a < 0 && b >= 0 ? true : false;
        }

        static public void PrintArray(int[] array, string text)
        {
            Console.WriteLine(text);
            foreach (var elem in array)
                Console.Write($"{elem}\t");
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(-20, 20);
            }
            PrintArray(arr, "Unordered array:");

            BubbleSorter.Sort(arr, Compare);
            PrintArray(arr, "\nOrdered array by ascending:");

            BubbleSorter.Sort(arr, Compare_descending);
            PrintArray(arr, "\nOrdered array by descending:");

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(-20, 20);
            }
            PrintArray(arr, "\nUnordered array:");
            BubbleSorter.Sort(arr, Compare_negative_positive);
            PrintArray(arr, "\nOrdered array by sign:");

            Console.WriteLine("\n");
        }
    }
}
