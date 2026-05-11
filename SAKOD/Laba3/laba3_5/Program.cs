using System;

namespace laba3_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Приоритетное включение");

            var q1 = new PriorityQueueInsert();

            q1.Enqueue(10, 1);
            q1.Enqueue(20, 3);
            q1.Enqueue(30, 2);

            q1.Print();

            Console.WriteLine("Удалён: " + q1.Dequeue());
            q1.Print();


            Console.WriteLine("\nПриоритетное исключение");

            var q2 = new PriorityQueueDelete();

            q2.Enqueue(10, 1);
            q2.Enqueue(20, 3);
            q2.Enqueue(30, 2);

            q2.Print();

            Console.WriteLine("Удалён: " + q2.Dequeue());
            q2.Print();
        }
    }
}
