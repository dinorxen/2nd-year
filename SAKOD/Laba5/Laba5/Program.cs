using System;
using System.Diagnostics;

namespace Laba5
{
    internal class Program
    {
        static int[] CreateArray(int n)
        {
            int[] array = new int[n];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i;
            }
            return array;
        }

        static int[] CreateKeys(int m, int n)
        {
            int[] keys = new int[m];
            Random random = new Random();
            for (int i = 0; i < keys.Length; i++)
            {
                keys[i] = random.Next(0, n * 2);
            }
            return keys;
        }

        static double MeasureSequential(int[] array, int[] keys)
        {
            Stopwatch sw = Stopwatch.StartNew();

            foreach (int key in keys)
            {
                SequentialSearch(array, key);
            }

            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureSequentialBarrier(int[] array, int[] keys)
        {
            Stopwatch sw = Stopwatch.StartNew();

            foreach (int key in keys)
            {
                SequentialSearchBarrier(array, key);
            }

            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureBinary(int[] array, int[] keys)
        {
            Stopwatch sw = Stopwatch.StartNew();

            foreach (int key in keys)
            {
                BinarySearch(array, key);
            }

            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureInterpolation(int[] array, int[] keys)
        {
            Stopwatch sw = Stopwatch.StartNew();

            foreach (int key in keys)
            {
                InterpolationSearch(array, key);
            }

            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static int SequentialSearch(int[] array, int key)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == key)
                {
                    return i;
                }
            }

            return -1;
        }

        static int SequentialSearchBarrier(int[] array, int key)
        {
            int[] newArray = new int[array.Length + 1];
            
            for (int j = 0; j < array.Length; j++)
            {
                newArray[j] = array[j];
            }

            newArray[array.Length] = key;
            int i = 0;

            while (newArray[i] != key)
            {
                i++;
            }

            if (i == array.Length)
            {
                return -1;
            }

            return i;
        }

        static int BinarySearch(int[] array, int key)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;

                if (array[middle] == key)
                {
                    return middle;
                }

                if (key > array[middle])
                {
                    left = middle + 1;
                }
                else
                {
                    right = middle - 1;
                }
            }

            return -1;
        }

        static int InterpolationSearch(int[] array, int key)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right && key >= array[left] && key <= array[right])
            {
                int position = left + (key - array[left]) * (right - left)
                               / (array[right] - array[left]);

                if (array[position] == key)
                {
                    return position;
                }

                if (array[position] < key)
                {
                    left = position + 1;
                }
                else
                {
                    right = position - 1;
                }
            }

            return -1;
        }

        static void Main(string[] args)
        {
            int[] size = { 1000, 2000, 4000, 8000, 16000 };
            int[] searches = { 5000, 10000, 20000 };

            Console.WriteLine("Последовательный и последовательный с барьером");

            foreach (int m in searches)
            {
                Console.WriteLine($"M = {m}");
                foreach (int n in size)
                {
                    int[] arr = CreateArray(n);
                    int[] keys = CreateKeys(m, n);

                    double t1 = MeasureSequential(arr, keys);
                    double t2 = MeasureSequentialBarrier(arr, keys);

                    Console.WriteLine($"N = {n}: обычный = {t1} мс, с барьером = {t2} мс");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Бинарный и интерполяционный\n");

            foreach (int m in searches)
            {
                Console.WriteLine($"M = {m}");

                foreach (int n in size)
                {
                    int[] array = CreateArray(n);
                    int[] keys = CreateKeys(m, n);

                    double t1 = MeasureBinary(array, keys);
                    double t2 = MeasureInterpolation(array, keys);

                    Console.WriteLine($"N={n}: бинарный = {t1} мс, интерполяционный = {t2} мс");
                }

                Console.WriteLine();
            }

        }
    }
}
