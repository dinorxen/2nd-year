using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ShellLab
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

        static List<int> ShellGaps(int n)
        {
            var gaps = new List<int>();
            for (int h = n / 2; h >= 1; h /= 2) gaps.Add(h);
            return gaps;
        }

        static List<int> HibbardGaps(int n)
        {
            var gaps = new List<int>();
            int k = 1;
            while ((1 << k) - 1 < n) { gaps.Add((1 << k) - 1); k++; }
            gaps.Reverse();
            return gaps;
        }

        static List<int> KnuthGaps(int n)
        {
            var gaps = new List<int>();
            int h = 1;
            while (h < n) { gaps.Add(h); h = 3 * h + 1; }
            gaps.Reverse();
            return gaps;
        }

        static List<int> SedgewickGaps(int n)
        {
            var gaps = new List<int>();
            int k = 0;
            while (true)
            {
                int gap = (k % 2 == 0)
                    ? 9 * ((1 << k) - (1 << (k / 2))) + 1
                    : 8 * (1 << k) - 6 * (1 << ((k + 1) / 2)) + 1;
                if (gap >= n) break;
                gaps.Add(gap);
                k++;
            }
            gaps.Reverse();
            return gaps;
        }

        static void ShellSort(int[] arr, List<int> gaps)
        {
            foreach (int h in gaps)
            {
                for (int i = h; i < arr.Length; i++)
                {
                    int key = arr[i];
                    int j = i;
                    while (j >= h && arr[j - h] > key)
                    {
                        arr[j] = arr[j - h];
                        j -= h;
                    }
                    arr[j] = key;
                }
            }
        }

        static double MeasureShell(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShellSort(array, ShellGaps(array.Length));
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureHibbard(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShellSort(array, HibbardGaps(array.Length));
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureKnuth(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShellSort(array, KnuthGaps(array.Length));
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static double MeasureSedgewick(int[] data)
        {
            int[] array = (int[])data.Clone();
            Stopwatch sw = Stopwatch.StartNew();
            ShellSort(array, SedgewickGaps(array.Length));
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        static void Main(string[] args)
        {
            int[] sizes = { 25000, 50000, 100000 };

            Console.WriteLine("Сортировка Шелла с разными последовательностями h\n");
            foreach (int n in sizes)
            {
                int[] arr = CreateArray(n);
                Console.WriteLine($"N = {n}: Шелла = {MeasureShell(arr):F2} мс, Хиббарда = {MeasureHibbard(arr):F2} мс, Кнута = {MeasureKnuth(arr):F2} мс, Седжвика = {MeasureSedgewick(arr):F2} мс");
            }
            Console.WriteLine();
        }
    }
}