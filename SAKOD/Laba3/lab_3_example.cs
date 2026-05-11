using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 1. В зависимости от чётности варианта выбираете односвязный или двусвязный
// 2. Сами выбираете процедурный или объектный стиль (предпочтительнее объектный)
// 3. Копируете и используете содержимое соответствующего namespace в ваш код (только все ненужное удалите и автоотформатируйте)
// 4. Задания оформляете статическим методом для процедурного стиля и методом для объектного по аналогии
// 5. Обязательна проверка на ошибки посредством кидания исключений (кроме проверки list != null, она большую часть кода займет)

namespace lab3_SinglyLinkedList1
{
	#region Односвязный список, процедурный стиль
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
		//тут задания, в двусвязном аналогично			
		public static void add_first(List list, int value) {
			Node node = new Node();
			node.next = list.first;
			node.value = value;
			list.first = node;
		}
		public static void delete_first(List list) {
			if (list.first == null) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}
			list.first = list.first.next;
		}
		public static void print(List list) {
			var first = true;
			for (var node = list.first; node != null; node = node.next) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(node.value);
			}
			Console.WriteLine();
		}

		//пример метода с параметром типа Node
		public static void dummy_delete_after(List list, Node node) {

		}
		public static void Debug() {
			var list = new List();
			print(list); // пустая строка
			add_first(list, 3);
			add_first(list, 2);
			add_first(list, 1);
			print(list); // 1 2 3

			//пример использования метода с параметром типа Node
			dummy_delete_after(list, list.first.next.next);

			delete_first(list);
			print(list); // 2 3				

			delete_first(list);
			delete_first(list);
			//сейчас list пустой
			//попробуем удалить первый эдемент из пустого списка
			try {
				delete_first(list);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message); //нельзя удалить первый элемент из пустого списка
			}

			//тут вы проверяете работоспособность написанного кода
		}
	}
	#endregion
}
namespace lab3_SinglyLinkedList2
{
	#region Односвязный список, объектный стиль
	public class List
	{
		public class Node
		{
			public int value;
			public Node next;
		}

		public Node first;

		//тут задания, в двусвязном аналогично
		//отличается от процедурного тем, что вместо первого аргумента (list) пишем this
		//кстати вместо this.bla можно писать просто bla, если нет локальной переменной или параметра с именем bla
		public void add_first(int value) {
			Node node = new Node();
			node.next = this.first;
			node.value = value;
			this.first = node;
		}

		//можно предыдущее задание написать так (если вы понимаете, что тут написано)
		public void add_first2(int value) {
			first = new Node() { value = value, next = first };
		}

		//тут без this
		public void delete_first() {
			if (first == null) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}

			first = first.next;
		}
		//пример метода с параметром типа Node
		public void dummy_delete_after(Node node) {

		}
		public void print() {
			var first = true;
			for (var node = this.first; node != null; node = node.next) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(node.value);
			}
			Console.WriteLine();
		}
	}

	//отладка в отдельном классе, ибо ей не место в самом списке
	public static class ListStuff
	{
		public static void Debug() {
			var list = new List();
			list.print(); // пустая строка
			list.add_first(3);
			list.add_first(2);
			list.add_first(1); //советую тут поставить breakpoint (F9) и посмотреть, что хранится в list

			list.print(); // 1 2 3

			//пример использования метода с параметром типа Node
			list.dummy_delete_after(list.first.next.next);

			list.delete_first();
			list.print(); // 2 3				
		}
	}
	#endregion
}
namespace lab3_DoublyLinkedList1
{
	#region Двусвязный список, процедурный стиль
	public class Node
	{
		public int value;
		public Node next;
		public Node prev;
	}
	public class List
	{
		public Node first;
	}
	public static class ListStuff
	{
		public static void add_first(List list, int value) {
			Node node = new Node();
			node.next = list.first;
			node.value = value;
			list.first = node;
			if (list.first.next != null) {
				list.first.next.prev = list.first;
			}
		}
		public static void delete_first(List list) {
			if (list.first == null) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}
			list.first = list.first.next;
			if (list.first != null) {
				list.first.prev = null;
			}
		}
		public static void print(List list) {
			var first = true;
			for (var node = list.first; node != null; node = node.next) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(node.value);
			}
			Console.WriteLine();
		}
		public static void Debug() {
			var list = new List();
			print(list); // пустая строка
			add_first(list, 3);
			add_first(list, 2);
			add_first(list, 1);
			//советую тут поставить breakpoint (F9) и посмотреть, что хранится в list
			print(list); // 1 2 3

			delete_first(list);
			print(list); // 2 3
		}
	}
	#endregion
}
namespace lab3_DoublyLinkedList2
{
	#region Двусвязный список, объектный стиль
	public class List
	{
		public class Node
		{
			public int value;
			public Node next;
			public Node prev;
		}

		public Node first;

		public void add_first(int value) {
			Node node = new Node();
			node.next = this.first;
			node.value = value;
			this.first = node;
			if (this.first.next != null) {
				this.first.next.prev = this.first;
			}
		}
		public void delete_first() {
			if (first == null) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}
			first = first.next;
			if (first != null) {
				first.prev = null;
			}
		}
		public void print() {
			var first = true;
			for (var node = this.first; node != null; node = node.next) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(node.value);
			}
			Console.WriteLine();
		}
	}
	public static class ListStuff
	{
		public static void Debug() {
			var list = new List();
			list.print(); // пустая строка
			list.add_first(3);
			list.add_first(2);
			list.add_first(1);
			list.print(); // 1 2 3

			list.delete_first();
			list.print(); // 2 3
		}
	}
	#endregion
}

namespace lab3_DoublyLinkedList_ArrayOfStructures
{
	#region Двусвязный список, объектный стиль, массив структур
	public class List
	{
		const int nil = -1;
		public class Node
		{
			public double value;
			public int next = nil;
			public int prev = nil;
		}

		List<Node> nodes = new List<Node>();
		public int first = nil;

		public void add_first(double value) {
			Node node = new Node();
			node.next = this.first;
			node.value = value;
			this.first = nodes.Count;
			nodes.Add(node);
			if (nodes[this.first].next != nil) {
				nodes[nodes[this.first].next].prev = this.first;
			}
		}
		public void delete_first() {
			if (first == nil) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}
			first = nodes[first].next;
			if (first != nil) {
				nodes[first].prev = nil;
			}
		}
		public void print() {
			var first = true;
			for (var node = this.first; node != nil; node = nodes[node].next) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(nodes[node].value);
			}
			Console.WriteLine();
		}
	}
	public static class ListStuff
	{
		public static void Debug() {
			var list = new List();
			list.print(); // пустая строка
			list.add_first(3);
			list.add_first(2);
			list.add_first(1);
			list.print(); // 1 2 3

			list.delete_first();
			list.print(); // 2 3
		}
	}
	#endregion
}
namespace lab3_DoublyLinkedList_StructureOfArrays
{
	#region Двусвязный список, объектный стиль, структура массивов
	public class List
	{
		List<int> next = new List<int>();
		List<int> prev = new List<int>();
		List<double> values = new List<double>();

		const int nil = -1;

		public int first = nil;

		public void add_first(double value) {
			int node_next = this.first;
			double node_value = value;
			this.first = values.Count;
			values.Add(node_value);
			next.Add(node_next);
			prev.Add(nil);
			if (next[this.first] != nil) {
				prev[next[this.first]] = this.first;
			}
		}
		public void delete_first() {
			if (first == nil) {
				throw new Exception("нельзя удалить первый элемент из пустого списка");
			}
			first = next[first];
			if (first != nil) {
				prev[first] = nil;
			}
		}
		public void print() {
			var first = true;
			for (var node = this.first; node != nil; node = next[node]) {
				if (!first) {
					Console.Write(" ");
				}
				first = false;
				Console.Write(values[node]);
			}
			Console.WriteLine();
		}
	}
	public static class ListStuff
	{
		public static void Debug() {
			var list = new List();
			list.print(); // пустая строка
			list.add_first(3);
			list.add_first(2);
			list.add_first(1);
			list.print(); // 1 2 3

			list.delete_first();
			list.print(); // 2 3
		}
	}
	#endregion
}


class Program
{
	static void Main() {
		lab2_SinglyLinkedList1.ListStuff.Debug();
		lab2_SinglyLinkedList2.ListStuff.Debug();
		lab2_DoublyLinkedList1.ListStuff.Debug();
		lab2_DoublyLinkedList2.ListStuff.Debug();
		lab2_DoublyLinkedList_ArrayOfStructures.ListStuff.Debug();
		lab2_DoublyLinkedList_StructureOfArrays.ListStuff.Debug();
	}
}

