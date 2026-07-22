using System;
using System.Diagnostics;

namespace laba8_2
{
    internal class Program
    {
        static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
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


        static void Heapify(int[] a, int n, int root)
        {
            while (true)
            {
                int largest = root;
                int left = 2 * root + 1;
                int right = 2 * root + 2;

                if (left < n && a[left] > a[largest]) largest = left;
                if (right < n && a[right] > a[largest]) largest = right;

                if (largest == root) return;

                Swap(a, root, largest);
                root = largest;
            }
        }

        static void HeapSort(int[] a)
        {
            int n = a.Length;
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(a, n, i);

            for (int i = n - 1; i > 0; i--)
            {
                Swap(a, 0, i);
                Heapify(a, i, 0);
            }
        }


        static void RadixMSD(int[] a)
        {
            RadixMSD(a, 0, a.Length - 1, 3);
        }

        static void RadixMSD(int[] a, int lo, int hi, int byteIndex)
        {
            if (lo >= hi || byteIndex < 0)
                return;

            int shift = byteIndex * 8;

            int[] count = new int[257];
            for (int i = lo; i <= hi; i++)
            {
                int b = (a[i] >> shift) & 0xFF;
                count[b + 1]++;
            }

            for (int i = 0; i < 256; i++)
                count[i + 1] += count[i];

            int[] temp = new int[hi - lo + 1];
            int[] pos = (int[])count.Clone();
            for (int i = lo; i <= hi; i++)
            {
                int b = (a[i] >> shift) & 0xFF;
                temp[pos[b]++] = a[i];
            }

            for (int i = 0; i < temp.Length; i++)
                a[lo + i] = temp[i];

            for (int i = 0; i < 256; i++)
            {
                int start = lo + count[i];
                int end = lo + count[i + 1] - 1;
                if (start < end)
                    RadixMSD(a, start, end, byteIndex - 1);
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


        static double MeasureRadix(int[] source, int repeats)
        {
            double total = 0;
            for (int r = 0; r < repeats; r++)
            {
                int[] a = (int[])source.Clone();
                Stopwatch sw = Stopwatch.StartNew();
                RadixMSD(a);
                sw.Stop();
                total += sw.Elapsed.TotalMilliseconds;
            }
            return total / repeats;
        }

        static double MeasureHeap(int[] source, int repeats)
        {
            double total = 0;
            for (int r = 0; r < repeats; r++)
            {
                int[] a = (int[])source.Clone();
                Stopwatch sw = Stopwatch.StartNew();
                HeapSort(a);
                sw.Stop();
                total += sw.Elapsed.TotalMilliseconds;
            }
            return total / repeats;
        }

        static double MeasureQuick(int[] source, int repeats)
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

        static void Main(string[] args)
        {
            Random rnd = new Random(12345);
            const int repeats = 3;

            int n1 = 50000;
            int n2 = 100000;
            int n3 = 200000;

            int[] rand1 = CreateRandom(n1, rnd);
            int[] rand2 = CreateRandom(n2, rnd);
            int[] rand3 = CreateRandom(n3, rnd);
            int[] asc = CreateAscending(n3);
            int[] desc = CreateDescending(n3);

            Console.WriteLine("Поразрядная (по старшему разряду):");
            Console.WriteLine($"N=50000: {MeasureRadix(rand1, repeats):F2} мс");
            Console.WriteLine($"N=100000: {MeasureRadix(rand2, repeats):F2} мс");
            Console.WriteLine($"N=200000: {MeasureRadix(rand3, repeats):F2} мс");
            Console.WriteLine($"N=200000 неубыв.: {MeasureRadix(asc, repeats):F2} мс");
            Console.WriteLine($"N=200000 невозр.: {MeasureRadix(desc, repeats):F2} мс");

            Console.WriteLine("\nПирамидальная:");
            Console.WriteLine($"N=50000: {MeasureHeap(rand1, repeats):F2} мс");
            Console.WriteLine($"N=100000: {MeasureHeap(rand2, repeats):F2} мс");
            Console.WriteLine($"N=200000: {MeasureHeap(rand3, repeats):F2} мс");
            Console.WriteLine($"N=200000 неубыв.: {MeasureHeap(asc, repeats):F2} мс");
            Console.WriteLine($"N=200000 невозр.: {MeasureHeap(desc, repeats):F2} мс");

            Console.WriteLine("\nБыстрая рекурсивная:");
            Console.WriteLine($"N=50000: {MeasureQuick(rand1, repeats):F2} мс");
            Console.WriteLine($"N=100000: {MeasureQuick(rand2, repeats):F2} мс");
            Console.WriteLine($"N=200000: {MeasureQuick(rand3, repeats):F2} мс");
            Console.WriteLine($"N=200000 неубыв.: {MeasureQuick(asc, repeats):F2} мс");
            Console.WriteLine($"N=200000 невозр.: {MeasureQuick(desc, repeats):F2} мс");
        }
    }
}
