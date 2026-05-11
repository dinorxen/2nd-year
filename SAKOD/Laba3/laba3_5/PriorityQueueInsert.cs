using System;
using System.Collections.Generic;
using System.Text;

namespace laba3_5
{
    class NodeInsert
    {
        public int value;
        public int priority;
        public NodeInsert next;
    }

    public class PriorityQueueInsert
    {
        private NodeInsert first;

        public void Enqueue(int value, int priority)
        {
            NodeInsert newNode = new NodeInsert();
            newNode.value = value;
            newNode.priority = priority;

            if (first == null || priority > first.priority)
            {
                newNode.next = first;
                first = newNode;
                return;
            }

            NodeInsert current = first;

            while (current.next != null && current.next.priority >= priority)
            {
                current = current.next;
            }

            newNode.next = current.next;
            current.next = newNode;
        }

        public int Dequeue()
        {
            if (first == null)
                throw new Exception("Очередь пуста");

            int value = first.value;
            first = first.next;
            return value;
        }

        public void Print()
        {
            NodeInsert current = first;

            while (current != null)
            {
                Console.WriteLine($"Значение: {current.value}, приоритет: {current.priority}");
                current = current.next;
            }
        }
    }
}
