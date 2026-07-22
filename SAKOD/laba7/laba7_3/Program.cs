using System;
using System.Diagnostics;

namespace Laba7Records
{
    class Record
    {
        public int Key;                        
        public string? Info;                         
        public long F1, F2, F3, F4, F5, F6, F7;      
    }

    internal class Program
    {
        static Record[] CreateArray(int n)
        {
            Record[] array = new Record[n];
            Random random = new Random();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Record { Key = random.Next(1000000), Info = "info_" + i };
            }
            return array;
        }

        static Record[] CreateAscending(int n)
        {
            Record[] array = CreateArray(n);
            Array.Sort(array, (a, b) => a.Key.CompareTo(b.Key));
            return array;
        }

        static Record[] CreateDescending(int n)
        {
            Record[] array = CreateAscending(n);
            Array.Reverse(array);
            return array;
        }

        static double MeasureBubble(Record[] data)
        {
            Record[] array = (Record[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            BubbleSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureShaker(Record[] data)
        {
            Record[] array = (Record[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShakerSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureSelection(Record[] data)
        {
            Record[] array = (Record[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            SelectionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureInsertion(Record[] data)
        {
            Record[] array = (Record[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            InsertionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureBinaryInsertion(Record[] data)
        {
            Record[] array = (Record[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            BinaryInsertionSort(array);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static void BubbleSort(Record[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                bool swapped = false;
                for (int j = 0; j < arr.Length - i - 1; j++)
                {
                    if (arr[j].Key > arr[j + 1].Key)
                    {
                        Record temp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }

        static void ShakerSort(Record[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            bool swapped = true;
            while (left < right && swapped)
            {
                swapped = false;
                for (int i = left; i < right; i++)
                {
                    if (arr[i].Key > arr[i + 1].Key)
                    {
                        Record temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                        swapped = true;
                    }
                }
                right--;
                for (int i = right; i > left; i--)
                {
                    if (arr[i - 1].Key > arr[i].Key)
                    {
                        Record temp = arr[i - 1];
                        arr[i - 1] = arr[i];
                        arr[i] = temp;
                        swapped = true;
                    }
                }
                left++;
            }
        }

        static void SelectionSort(Record[] arr)
        {
            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minidx = i;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j].Key < arr[minidx].Key)
                    {
                        minidx = j;
                    }
                }
                (arr[i], arr[minidx]) = (arr[minidx], arr[i]);
            }
        }

        static void InsertionSort(Record[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                Record key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j].Key > key.Key)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }
                arr[j + 1] = key;
            }
        }

        static void BinaryInsertionSort(Record[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                Record key = arr[i];
                int l = 0, r = i;
                while (l < r)
                {
                    int mid = (l + r) / 2;
                    if (arr[mid].Key <= key.Key)
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
            Record[] asc = CreateAscending(20000);
            Record[] desc = CreateDescending(20000);

            Console.WriteLine("Пузырьком, шейкером и выбором\n");
            foreach (int n in sizes)
            {
                Record[] arr = CreateArray(n);
                Console.WriteLine($"N = {n}: пузырьком = {MeasureBubble(arr):F2} мс, шейкер = {MeasureShaker(arr):F2} мс, выбором = {MeasureSelection(arr):F2} мс");
            }
            Console.WriteLine($"N = 20000 неуб.: пузырьком = {MeasureBubble(asc):F2} мс, шейкер = {MeasureShaker(asc):F2} мс, выбором = {MeasureSelection(asc):F2} мс");
            Console.WriteLine($"N = 20000 невозр.: пузырьком = {MeasureBubble(desc):F2} мс, шейкер = {MeasureShaker(desc):F2} мс, выбором = {MeasureSelection(desc):F2} мс");
            Console.WriteLine();

            Console.WriteLine("Вставками и бинарными вставками\n");
            foreach (int n in sizes)
            {
                Record[] arr = CreateArray(n);
                Console.WriteLine($"N = {n}: вставками = {MeasureInsertion(arr):F2} мс, бинарными = {MeasureBinaryInsertion(arr):F2} мс");
            }
            Console.WriteLine($"N = 20000 неуб.: вставками = {MeasureInsertion(asc):F2} мс, бинарными = {MeasureBinaryInsertion(asc):F2} мс");
            Console.WriteLine($"N = 20000 невозр.: вставками = {MeasureInsertion(desc):F2} мс, бинарными = {MeasureBinaryInsertion(desc):F2} мс");
            Console.WriteLine();
        }
    }
}