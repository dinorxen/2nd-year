using System;
using System.Net;
namespace Laba3
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

        public static void delete_first(List list)
        {
            if (list.first == null)
            {
                Console.WriteLine("нельзя удалить первый элемент из пустого списка");
                return;
            }
            list.first = list.first.next;
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

        public static void delete_after(List list, Node node)
        {
            if (list.first == null)
            {
                throw new Exception("список пуст");
            }
            if (node == null)
            {
                throw new Exception("узел не существует");
            }
            if (node.next == null)
            {
                throw new Exception("после заданого узла нет узла для удаления");
            }
            node.next = node.next.next;
        }

        public static void insert_before(List list, Node node, int value)
        {
            if (list.first == null)
            {
                throw new Exception("нельзя вставить перед элементом в пустом списке");
            }
            if (node == null)
            {
                throw new Exception("заданый узел не существует");
            }
            if (list.first == node)
            {
                add_first(list, value);
                return;
            }
            
            Node current = list.first;
            while (current != null && current.next != node)
            {
                current = current.next;
            }
            if (current == null)
            {
                throw new Exception("заданный элемент не принадлежит списку");
            }

            Node NewNode = new Node();
            NewNode.value = value;
            NewNode.next = node;
            current.next = NewNode;
        }

        public static Node find(List list, int value)
        {
            for (Node node = list.first; node != null; node = node.next)
            {
                if (node.value == value)
                {
                    return node;
                }
            }
            return null;
        }

        public static int get_length(List list)
        {
            int count = 0;
            for (Node node = list.first; node != null; node = node.next)
            {
                count++;
            }
            return count;
        }

        public static void reverse(List list)
        {
            Node prev = null;
            Node current = list.first;
            while (current != null) 
            {
                Node next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }
            list.first = prev;
        }

        public static int get_min(List list)
        {
            if (list.first == null)
            {
                throw new Exception("нельзя найти минимум в пустом списке");
            }
            int min = list.first.value;
            for (Node node = list.first; node != null; node = node.next)
            {
                if (node.value <= min)
                {
                    min = node.value;
                }
            }
            return min;
        }

        public static void Debug()
        {
            Console.WriteLine("Пустой список");
            List empty = new List();
            Console.WriteLine("Список:");
            print(empty);
            Console.WriteLine($"Длинна списка: {get_length(empty)}");
            try
            {
                Console.WriteLine($"Минимум: {get_min(empty)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("-----------------------");
            
            Console.WriteLine("Список из одного элемента");
            List one = new List();
            add_first(one, 10);
            Console.WriteLine("Список:");
            print(one);
            Console.WriteLine("Длина: " + get_length(one));
            Console.WriteLine("Минимум: " + get_min(one));
            Node findOne = find(one, 10);
            Console.WriteLine(findOne != null ? "Элемент 10 найден" : "Элемент 10 не найден");
            reverse(one);
            print(one);
            try
            {
                delete_after(one, one.first);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("-----------------------");
            
            Console.WriteLine("Список из нескольких элементов");
            List list = new List();
            add_first(list, 1);
            add_first(list, 2);
            add_first(list, 3);
            add_first(list, 4);
            add_first(list, -3);
            Console.WriteLine("Список:");
            print(list);
            Console.WriteLine("Длина: " + get_length(list));
            Console.WriteLine("Минимум: " + get_min(list));
            Node findNum = find(list, 2);
            Console.WriteLine(findOne != null ? "Элемент 2 найден" : "Элемент 2 не найден");
            Node findNoNum = find(list, 10);
            Console.WriteLine(findOne != null ? "Элемент 10 найден" : "Элемент 10 не найден");
            Console.WriteLine("Вставка 55 перед элементом 3");
            insert_before(list, find(list, 3), 55);
            print(list);
            Console.WriteLine("Удаление элемента 55");
            delete_after(list, find(list, 4));
            print(list);
            Console.WriteLine("Разворот списка");
            reverse(list);
            print(list);
            Console.WriteLine("Вставка перед первым элементом");
            insert_before(list, list.first, 100);
            print(list);
        }
    }
    public class Program
    {
        public static void Main()
        {
            ListStuff.Debug();
        }
    }
}
