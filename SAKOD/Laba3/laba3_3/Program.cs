using System.Diagnostics;
using System.Net;

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
            var node = new Node();
            node.value = value;
            node.next = list.first;
            list.first = node;
        }

        public static void print(List list)
        {
            var first = true;
            for (var node = list.first; node != null; node = node.next)
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

        public static int IosifList(List list, int k)
        {
            if (list.first == null)
            {
                Console.WriteLine("Список пуст");
                return -1;
            }
            Node tail = list.first;
            while (tail.next != null)
            {
                tail = tail.next;
            }
            tail.next = list.first;
            Node prev = tail;
            Node current = list.first;

            while (current.next != current)
            {
                for (int i = 1; i < k; i++)
                {
                    prev = current;
                    current = current.next;
                }

                if (current == list.first)
                {
                    list.first = current.next;
                }

                prev.next = current.next;
                current = current.next;
            }
            list.first = current;
            list.first.next = null;
            return current.value;
        }
    }
    internal class Program
    {
        public static int IosifArr(int n, int k)
        {
            int[] arr = new int[n];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i + 1;
            }
            int size = n;
            int index = 0;
            while (size > 1)
            {
                index = (index + k - 1) % size;
                for (int i = index; i < size - 1; i++)
                {
                    arr[i] = arr[i + 1];
                }
                size--;
            }
            return arr[0];
        }
        static void Main(string[] args)
        {
            int[] n = { 1000, 5000, 10000 };
            Stopwatch sw = new Stopwatch();

            Console.WriteLine("Массив");
            foreach (int i in n)
            {
                sw.Restart();
                var result = IosifArr(i, 3);
                sw.Stop();
                Console.WriteLine($"n = {i}");
                Console.WriteLine($"Выживший: {result}");
                Console.WriteLine($"Время: {sw.Elapsed.TotalMilliseconds:F6} мс\n");
            }
            Console.WriteLine("-----------------------\n");
            Console.WriteLine("Связаный список");
            foreach (int i in n)
            {
                List list = new List();
                for (int j = i; j >= 1; j--)
                {
                    ListStuff.add_first(list, j);
                }
                sw.Restart();
                int result = ListStuff.IosifList(list, 3);
                sw.Stop();
                Console.WriteLine($"n = {i}");
                Console.WriteLine($"Выживший: {result}");
                Console.WriteLine($"Время: {sw.Elapsed.TotalMilliseconds:F6} мс\n");
            }
        }
    }
}