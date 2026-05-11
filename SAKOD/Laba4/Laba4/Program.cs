using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laba4
{
    class Node
    {
        public int Value;
        public Node Left, Right;
        public bool IsExternal;

        public Node(int value) { Value = value; }
        public Node() { IsExternal = true; Value = -1; }
    }

    class Program
    {
        static int CountLeaves(Node node)
        {
            if (node == null)
                return 0;

            if (node.Left == null && node.Right == null)
                return 1;

            return CountLeaves(node.Left) + CountLeaves(node.Right);
        }
        static Node BuildBalanced(int[] data, int lo, int hi)
        {
            if (lo > hi) return null;
            int mid = (lo + hi) / 2;
            var node = new Node(data[mid]);
            node.Left = BuildBalanced(data, lo, mid - 1);
            node.Right = BuildBalanced(data, mid + 1, hi);
            return node;
        }
        static int Height(Node node)
        {
            if (node == null) return 0;
            return 1 + Math.Max(Height(node.Left), Height(node.Right));
        }
        static int Levels(Node root) => Height(root);
        static List<List<int>> pathsLen3 = new List<List<int>>();

        static void FindPathsOf3(Node node, List<int> current)
        {
            if (node == null) return;
            current.Add(node.Value);

            if (current.Count == 3)
                pathsLen3.Add(new List<int>(current));
            else
            {
                FindPathsOf3(node.Left, current);
                FindPathsOf3(node.Right, current);
            }

            current.RemoveAt(current.Count - 1);
        }
        static Node BuildExtended(Node node)
        {
            if (node == null) return new Node(); 

            var ext = new Node(node.Value);
            ext.Left = BuildExtended(node.Left);
            ext.Right = BuildExtended(node.Right);
            return ext;
        }
        static (int ipl, int epl) PathLengths(Node node, int depth = 0)
        {
            if (node == null) return (0, 0);

            if (node.IsExternal) return (0, depth);

            var (li, le) = PathLengths(node.Left, depth + 1);
            var (ri, re) = PathLengths(node.Right, depth + 1);
            return (li + ri + depth, le + re);
        }
        static void PrintTree(Node node, string prefix = "", bool isLeft = false)
        {
            if (node == null) return;
            string connector = isLeft ? "├── " : "└── ";
            string label = node.IsExternal ? "[□]" : node.Value.ToString();
            Console.WriteLine(prefix + connector + label);
            string childPrefix = prefix + (isLeft ? "│   " : "    ");
            if (node.Left != null || node.Right != null)
            {
                PrintTree(node.Left, childPrefix, true);
                PrintTree(node.Right, childPrefix, false);
            }
        }
        static int[] LoadOrCreateFile(string path)
        {
            if (!File.Exists(path))
            {
                var sample = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                File.WriteAllText(path, string.Join(" ", sample));
                Console.WriteLine($"Файл '{path}' создан с данными: {string.Join(", ", sample)}\n");
            }
            string text = File.ReadAllText(path).Trim();
            var parts = text.Split(new char[] { ' ', '\n', '\r', ',' }, StringSplitOptions.RemoveEmptyEntries);
            var result = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++) result[i] = int.Parse(parts[i]);
            Array.Sort(result);
            return result;
        }

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            const string dataFile = "nodes.txt";

            int[] data = LoadOrCreateFile(dataFile);
            Console.WriteLine($"Загружено узлов: {data.Length}");
            Console.WriteLine($"Данные: [{string.Join(", ", data)}]\n");

            Node root = BuildBalanced(data, 0, data.Length - 1);

            Console.WriteLine("Идеально сбалансированное дерево:");
            PrintTree(root, "", false);
            int h = Height(root);
            Console.WriteLine($"\nа) Высота дерева : {h}");

            int lv = Levels(root);
            Console.WriteLine($"б) Число уровней : {lv}");

            Console.WriteLine("\nв) Пути длины 3 (3 узла, 2 ребра):");
            pathsLen3.Clear();
            FindPathsOf3(root, new List<int>());
            if (pathsLen3.Count == 0)
                Console.WriteLine("(нет путей длиной 3)");
            else
                foreach (var p in pathsLen3)
                    Console.WriteLine("     " + string.Join(" → ", p));
            Console.WriteLine($"Итого: {pathsLen3.Count} путей");

            Node ext = BuildExtended(root);
            Console.WriteLine("  г) Расширенное дерево (□ = внешний узел):");
            PrintTree(ext, "", false);

            var (ipl, epl) = PathLengths(ext, 0);
            Console.WriteLine($"\nд) Длина внутреннего пути (IPL) : {ipl}");
            Console.WriteLine($"Длина внешнего  пути  (EPL) : {epl}");
            Console.WriteLine($"Проверка: EPL = IPL + 2·n = {ipl} + 2·{data.Length} = {ipl + 2 * data.Length}");
            Console.WriteLine();
            int leavesCount = CountLeaves(root);
            Console.WriteLine("Количество листьев: " + leavesCount);

            TreeTraversals traversals = new TreeTraversals();

            Console.WriteLine("\nНерекурсивные обходы дерева:");

            Console.Write("Префиксный обход: ");
            traversals.Preorder(root);

            Console.Write("\nИнфиксный обход: ");
            traversals.Inorder(root);

            Console.Write("\nПостфиксный обход: ");
            traversals.Postorder(root);

            Console.Write("\nПоуровневый обход: ");
            traversals.LevelOrder(root);

            Console.WriteLine();
        }
    }
}