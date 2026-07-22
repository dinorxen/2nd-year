using System;
using System.Diagnostics;

namespace laba8
{
    internal class Program
    {
        static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
        }
        static void InsertionSort(int[] a, int lo, int hi)
        {
            for (int i = lo + 1; i <= hi; i++)
            {
                int key = a[i];
                int j = i - 1;
                while (j >= lo && a[j] > key)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = key;
            }
        }
        static int Partition(int[] a, int lo, int hi)
        {
            int mid = lo + (hi - lo) / 2;
            if (a[mid] < a[lo]) Swap(a, lo, mid);
            if (a[hi] < a[lo]) Swap(a, lo, hi);
            if (a[hi] < a[mid]) Swap(a, mid, hi);
            Swap(a, mid, hi);
            int pivot = a[hi];
            int i = lo;
            for (int j = lo; j < hi; j++)
            {
                if (a[j] < pivot)
                {
                    Swap(a, i, j);
                    i++;
                }
            }
            Swap(a, i, hi);
            return i;
        }
        static void QuickRecursive(int[] a, int lo, int hi)
        {
            while (lo < hi)
            {
                int p = Partition(a, lo, hi);
                if (p - lo < hi - p)
                {
                    QuickRecursive(a, lo, p - 1);
                    lo = p + 1;
                }
                else
                {
                    QuickRecursive(a, p + 1, hi);
                    hi = p - 1;
                }
            }
        }
        static void QuickCombined(int[] a, int lo, int hi, int M)
        {
            while (lo < hi)
            {
                if (hi - lo + 1 < M)
                {
                    InsertionSort(a, lo, hi);
                    return;
                }
                int p = Partition(a, lo, hi);
                if (p - lo < hi - p)
                {
                    QuickCombined(a, lo, p - 1, M);
                    lo = p + 1;
                }
                else
                {
                    QuickCombined(a, p + 1, hi, M);
                    hi = p - 1;
                }
            }
        }

        static int[] CreateRandom(int n, Random rnd)
        {
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = rnd.Next();
            return a;
        }

        static int[] CreateAscending(int n)
        {
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = i;
            return a;
        }

        static int[] CreateDescending(int n)
        {
            int[] a = new int[n];
            for (int i = 0; i < n; i++)
                a[i] = n - i;
            return a;
        }

        static double MeasureRecursive(int[] source, int repeats)
        {
            double total = 0;
            for (int r = 0; r < repeats; r++)
            {
                int[] a = (int[])source.Clone();
                Stopwatch sw = Stopwatch.StartNew();
                QuickRecursive(a, 0, a.Length - 1);
                sw.Stop();
                total += sw.Elapsed.TotalMilliseconds;
            }
            return total / repeats;
        }

        static double MeasureCombined(int[] source, int M, int repeats)
        {
            double total = 0;
            for (int r = 0; r < repeats; r++)
            {
                int[] a = (int[])source.Clone();
                Stopwatch sw = Stopwatch.StartNew();
                QuickCombined(a, 0, a.Length - 1, M);
                sw.Stop();
                total += sw.Elapsed.TotalMilliseconds;
            }
            return total / repeats;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random(12345);
            const int repeats = 3;
            const int M = 30;

            int n1 = 250000;
            int n2 = 500000;
            int n3 = 1000000;

            int[] rand1 = CreateRandom(n1, rnd);
            int[] rand2 = CreateRandom(n2, rnd);
            int[] rand3 = CreateRandom(n3, rnd);
            int[] asc = CreateAscending(n3);
            int[] desc = CreateDescending(n3);

            Console.WriteLine("Рекурсивная:");
            Console.WriteLine($"N=2,5*10^5: {MeasureRecursive(rand1, repeats):F2}");
            Console.WriteLine($"N=5*10^5: {MeasureRecursive(rand2, repeats):F2}");
            Console.WriteLine($"N=10^6: {MeasureRecursive(rand3, repeats):F2}");
            Console.WriteLine($"N=10^6 неубыв.: {MeasureRecursive(asc, repeats):F2}");
            Console.WriteLine($"N=10^6 невозр.: {MeasureRecursive(desc, repeats):F2}");

            Console.WriteLine($"\nКомбинированная (M={M}):");
            Console.WriteLine($"N=2,5*10^5: {MeasureCombined(rand1, M, repeats):F2}");
            Console.WriteLine($"N=5*10^5: {MeasureCombined(rand2, M, repeats):F2}");
            Console.WriteLine($"N=10^6: {MeasureCombined(rand3, M, repeats):F2}");
            Console.WriteLine($"N=10^6 неубыв.: {MeasureCombined(asc, M, repeats):F2}");
            Console.WriteLine($"N=10^6 невозр.: {MeasureCombined(desc, M, repeats):F2}");
        }
    }
}
