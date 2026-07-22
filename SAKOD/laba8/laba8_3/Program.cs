using System;

namespace laba8_3
{
    internal class Program
    {
        static void Swap(int[] a, int i, int j)
        {
            int t = a[i];
            a[i] = a[j];
            a[j] = t;
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

        static int[] KLargest(int[] source, int k)
        {
            int n = source.Length;
            if (k < 0) k = 0;
            if (k > n) k = n;

            int[] a = (int[])source.Clone();

            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(a, n, i);

            int[] result = new int[k];
            int heapSize = n;
            for (int i = 0; i < k; i++)
            {
                result[i] = a[0];
                heapSize--;
                Swap(a, 0, heapSize);
                Heapify(a, heapSize, 0);
            }

            return result;
        }

        static void Main(string[] args)
        {
            string line = Console.ReadLine() ?? "";
            string[] parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            int[] array = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
                array[i] = int.Parse(parts[i]);

            int k = int.Parse(Console.ReadLine() ?? "0");

            int[] largest = KLargest(array, k);

            Console.WriteLine($"{k} наибольших элементов (по убыванию):");
            Console.WriteLine(string.Join(" ", largest));
        }
    }
}
