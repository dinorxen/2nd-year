using System;
using System.Collections.Generic;
using System.Text;

namespace laba3_5
{
    class NodeDelete
    {
        public int value;
        public int priority;
        public NodeDelete next;
    }

    public class PriorityQueueDelete
    {
        private NodeDelete first;

        public void Enqueue(int value, int priority)
        {
            NodeDelete newNode = new NodeDelete();
            newNode.value = value;
            newNode.priority = priority;

            if (first == null)
            {
                first = newNode;
                return;
            }

            NodeDelete current = first;

            while (current.next != null)
            {
                current = current.next;
            }

            current.next = newNode;
        }

        public int Dequeue()
        {
            if (first == null)
                throw new Exception("Очередь пуста");

            NodeDelete current = first;
            NodeDelete maxNode = first;

            NodeDelete previous = null;
            NodeDelete maxPrevious = null;

            while (current != null)
            {
                if (current.priority > maxNode.priority)
                {
                    maxNode = current;
                    maxPrevious = previous;
                }

                previous = current;
                current = current.next;
            }

            if (maxPrevious == null)
                first = first.next;
            else
                maxPrevious.next = maxNode.next;

            return maxNode.value;
        }

        public void Print()
        {
            NodeDelete current = first;

            while (current != null)
            {
                Console.WriteLine($"Значение: {current.value}, приоритет: {current.priority}");
                current = current.next;
            }
        }
    }
}
