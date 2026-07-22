using System;
using System.Net.Http.Headers;

namespace laba3_4
{
    public class Node
    {
        public int value;
        public Node next;
    }
    public class MyQueue
    {
        private Node first;
        private Node last;
        public void Enqueue(int value)
        {
            Node node = new Node();
            node.value = value;
            if (first == null)
            {
                first = last = node;
            }
            else
            {
                last.next = node;
                last = node;
            }
        }

        public int Dequeue()
        {
            if (first == null)
            {
                throw new Exception("очередь пуста");
            }
            int val = first.value;
            first = first.next;
            if (first == null)
            {
                last = null;
            }
            return val;
        }

        public void print()
        {
            Node cur = first;
            while (cur != null)
            {
                Console.Write(cur.value + " ");
                cur = cur.next;
            }
            Console.WriteLine();
        }

        public bool isEmpty()
        {
            return first == null;
        }
    }
    internal class Program
    {
        static Random random = new Random();

        static void printQueues(MyQueue[] queues)
        {
            Console.WriteLine("Состояние очередей:");
            for (int i = 0; i < queues.Length; i++)
            {
                Console.WriteLine($"Очередь: {i + 1}");
                if (queues[i].isEmpty())
                {
                    Console.WriteLine("Очередь пуста");
                }
                else
                {
                    queues[i].print();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("M (кол-во очередей): ");
            int m = int.Parse(Console.ReadLine());

            Console.Write("Кол-во операций: ");
            int ops = int.Parse(Console.ReadLine());

            MyQueue[] queues = new MyQueue[m];

            for (int i = 0; i < m; i++)
                queues[i] = new MyQueue();

            int clientId = 1;

            for (int i = 1; i <= ops; i++)
            {
                Console.WriteLine($"Операция {i}");

                int action = random.Next(2);

                if (action == 0)
                {
                    int q = random.Next(m);

                    queues[q].Enqueue(clientId);
                    Console.WriteLine($"Добавлен клиент #{clientId} в очередь {q + 1}");

                    clientId++;
                }
                else
                {
                    int q = random.Next(m);

                    if (!queues[q].isEmpty())
                    {
                        int served = queues[q].Dequeue();
                        Console.WriteLine($"Обслужен клиент #{served} из очереди {q + 1}");
                    }
                    else
                    {
                        Console.WriteLine($"Очередь {q + 1} пустая — обслуживания нет");
                    }
                }

                printQueues(queues);
            }
        }
    }
}