using System;
using System.Diagnostics;

namespace Laba7
{
    internal class Program
    {
        static int[] CreateArray(int n)
        {
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1000000);
            }
            return array;
        }

        static int[] CreateAscending(int n)
        {
            int[] array = CreateArray(n);
            Array.Sort(array);
            return array;
        }

        static int[] CreateDescending(int n)
        {
            int[] array = CreateAscending(n);
            Array.Reverse(array);
            return array;
        }

        static double MeasureBubble(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            BubbleSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureShaker(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShakerSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureSelection(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            SelectionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureInsertion(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            InsertionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureBinaryInsertion(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            BinaryInsertionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static void BubbleSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        int temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        static void ShakerSort(int[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        swapped = true;
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (arr[i - 1] > arr[i])
                    {
                        int temp = arr[i - 1];
                        arr[i - 1] = arr[i];
                        arr[i] = temp;
                        swapped = true;
                    }
                }
                left++;
            }
        }

        static void SelectionSort(int[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minidx = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minidx])
                    {
                        minidx = j;
                    }
                }
                (arr[i], arr[minidx]) = (arr[minidx], arr[i]);
            }
        }

        static void InsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        static void BinaryInsertionSort(int[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int l = 0, r = i;
                while (l < r)
                {
                    int mid = (l + r) / 2;
                    if (arr[mid] <= key)
                    {
                        l = mid + 1;
                    }
                    else
                    {
                        r = mid;
                    }
                }

                for (int j = i; j > l; j--) { arr[j] = arr[j - 1]; }
                arr[l] = key;
            }
        }

        static void Main(string[] args)
        {
            int[] sizes = { 5000, 10000, 20000 };
            int[] asc = CreateAscending(20000);
            int[] desc = CreateDescending(20000);

            Console.WriteLine("Пузырьком, шейкером и выбором\n");
            foreach (int n in sizes)
            {
                int[] arr = CreateArray(n);
                Console.WriteLine($"N = {n}: пузырьком = {MeasureBubble(arr):F2} мс, шейкер = {MeasureShaker(arr):F2} мс, выбором = {MeasureSelection(arr):F2} мс");
            }
            Console.WriteLine($"N = 20000 неуб.: пузырьком = {MeasureBubble(asc):F2} мс, шейкер = {MeasureShaker(asc):F2} мс, выбором = {MeasureSelection(asc):F2} мс");
            Console.WriteLine($"N = 20000 невозр.: пузырьком = {MeasureBubble(desc):F2} мс, шейкер = {MeasureShaker(desc):F2} мс, выбором = {MeasureSelection(desc):F2} мс");
            Console.WriteLine();

            Console.WriteLine("Вставками и бинарными вставками\n");
            foreach (int n in sizes)
            {
                int[] arr = CreateArray(n);
                Console.WriteLine($"N = {n}: вставками = {MeasureInsertion(arr):F2} мс, бинарными = {MeasureBinaryInsertion(arr):F2} мс");
            }
            Console.WriteLine($"N = 20000 неуб.: вставками = {MeasureInsertion(asc):F2} мс, бинарными = {MeasureBinaryInsertion(asc):F2} мс");
            Console.WriteLine($"N = 20000 невозр.: вставками = {MeasureInsertion(desc):F2} мс, бинарными = {MeasureBinaryInsertion(desc):F2} мс");
            Console.WriteLine();
        }
    }
}