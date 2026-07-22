using System;
using System.Diagnostics;

namespace laba3_3
{
    public class Node
    {
        public int value;
        public Node next;
    }

    public class List
    {
        public Node first;
    }

    public static class ListStuff
    {
        public static void add_first(List list, int value)
        {
            Node node = new Node();
            node.next = list.first;
            node.value = value;
            list.first = node;
        }

        public static void print(List list)
        {
            bool first = true;
            for (Node node = list.first; node != null; node = node.next)
            {
                if (!first)
                {
                    Console.Write(" ");
                }
                first = false;
                Console.Write(node.value);
            }
            Console.WriteLine();
        }

        public static void delete_after(Node prev)
        {
            if (prev == null || prev.next == null) return;
            prev.next = prev.next.next;
        }

        public static List create_numbers_list(int n)
        {
            List list = new List();

            for (int i = n; i >= 2; i--)
            {
                add_first(list, i);
            }

            return list;
        }
    }

    class Program
    {
        // Односвязный список
        static List EratosfenList(int n)
        {
            List list = ListStuff.create_numbers_list(n);
            Node currentPrime = list.first;

            while (currentPrime != null && currentPrime.value * currentPrime.value <= n)
            {
                int prime = currentPrime.value;
                Node prev = currentPrime;
                Node current = currentPrime.next;

                while (current != null)
                {
                    if (current.value % prime == 0)
                    {
                        ListStuff.delete_after(prev);
                        current = prev.next;
                    }
                    else
                    {
                        prev = current;
                        current = prev.next;
                    }
                }

                currentPrime = currentPrime.next;
            }

            return list;
        }

        // Массив
        static int[] EratosfenArray(int n)
        {
            bool[] isPrime = new bool[n + 1];
            System.Collections.Generic.List<int> prime = new System.Collections.Generic.List<int>();

            for (int i = 2; i <= n; i++)
                isPrime[i] = true;

            for (int p = 2; p * p <= n; p++)
            {
                if (isPrime[p])
                {
                    for (int i = p * p; i <= n; i += p)
                    {
                        isPrime[i] = false;
                    }
                }
            }

            for (int i = 2; i <= n; i++)
            {
                if (isPrime[i])
                    prime.Add(i);
            }

            return prime.ToArray();
        }

        static void PrintArr(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
            }
            Console.WriteLine();
        }

        static void Main()
        {
            int[] tests = { 10000, 50000, 100000 };

            foreach (int n in tests)
            {
                Console.WriteLine($"N = {n}");
                Stopwatch sw = new Stopwatch();

                sw.Start();
                int[] arr = EratosfenArray(n);
                sw.Stop();
                double time = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine($"Массив: {time:F4} мс");

                sw.Restart();
                List list = EratosfenList(n);
                sw.Stop();
                double time2 = sw.Elapsed.TotalMilliseconds;
                Console.WriteLine($"Односвязный список: {time2:F4} мс");

                Console.WriteLine();
            }

            Console.WriteLine("Проверка для N = 100:");
            int[] testArr = EratosfenArray(100);
            PrintArr(testArr);

            List testList = EratosfenList(100);
            ListStuff.print(testList);
        }
    }
}