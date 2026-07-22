using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Laba4
{
    class Node
    {
        public int value;
        public Node right, left;
        public Node(int value) { this.value = value; }
    }

    class Tree
    {
        public Node root;
        public Tree(Queue<int> source)
        {
            root = BuildBalanced(source, source.Count);
        }
        private Node BuildBalanced(Queue<int> source, int n)
        {
            if (n == 0) { return null; }

            int nLeft = n / 2;
            int nRight = n - nLeft - 1;

            Node Left = BuildBalanced(source, nLeft);
            Node node = new Node(source.Dequeue());
            node.left = Left;
            node.right = BuildBalanced(source, nRight);
            
            return node;
        }
        public ExtTree Extend()
        {
            return new ExtTree(root);
        }
        public int Height()
        {
            return Height(root);
        }
        private int Height(Node node)
        {
            if (node == null) { return -1; }
            return 1 + Math.Max(Height(node.left), Height(node.left));
        }
        public int LevelCount()
        {
            return Height() + 1;
        }

        public List<string> Paths3()
        {
            List<string> paths = new List<string>();
            CollectPaths3(root, paths);
            return paths;
        }

        private static void CollectPaths3(Node node, List<string> paths)
        {
            if (node == null) return;

            foreach (Node c in Children(node))           
                foreach (Node g in Children(c))         
                    foreach (Node gg in Children(g))    
                        paths.Add(node.value + " " + c.value + " " + g.value + " " + gg.value);

            CollectPaths3(node.left, paths);
            CollectPaths3(node.right, paths);
        }

        private static List<Node> Children(Node node)
        {
            List<Node> list = new List<Node>();
            if (node.left != null) list.Add(node.left);
            if (node.right != null) list.Add(node.right);
            return list;
        }
    }

    class ExtNode
    {
        public string? Label;
        public bool IsExtrenal;
        public ExtNode? left, right;
        public ExtNode(string Label, bool IsExtrenal) 
        {
            this.Label = Label;
            this.IsExtrenal = IsExtrenal;
        }
    }

    class ExtTree
    {
        private ExtNode Root;

        public ExtTree(Node binaryRoot)
        {
            Root = Build(binaryRoot);
        }

        public int InternalPathLength()
        {
            return InternalSum(Root, 1);
        }

        public int ExternalPathLength()
        {
            return ExternalSum(Root, 1);
        }

        private static ExtNode Build(Node node)
        {
            if (node == null)
                return new ExtNode("[]", true); 

            ExtNode e = new ExtNode(node.value.ToString(), false);  
            e.left = Build(node.left);
            e.right = Build(node.right);
            return e;
        }

        private static int InternalSum(ExtNode node, int level)
        {
            if (node.IsExtrenal) return 0;
            return level + InternalSum(node.left, level + 1) + InternalSum(node.right, level + 1);
        }

        private static int ExternalSum(ExtNode node, int level)
        {
            if (node.IsExtrenal) return level;    
            return ExternalSum(node.left, level + 1) + ExternalSum(node.right, level + 1);
        }
    }
    class Program
    {
        static void Main()
        {
            string path = "input.txt";
            if (!File.Exists(path))
                File.WriteAllText(path, "1 2 3 4 5 6 7 8 9 10");

            string text = File.ReadAllText(path);
            string[] parts = text.Split(new char[] { ' ', '\t', '\n', '\r', ',', ';' },
                                        StringSplitOptions.RemoveEmptyEntries);

            Queue<int> source = new Queue<int>();
            foreach (string part in parts)
                source.Enqueue(int.Parse(part));

            int N = source.Count;

            Tree tree = new Tree(source);
            ExtTree ext = tree.Extend();

            Console.WriteLine("Прочитано N = " + N + " узлов");
            Console.WriteLine("a) Высота дерева:  " + tree.Height());
            Console.WriteLine("b) Число уровней: " + tree.LevelCount());

            List<string> paths = tree.Paths3();
            Console.WriteLine("c) Путей длины 3: " + paths.Count);
            foreach (string p in paths)
                Console.WriteLine("   " + p);

            Console.WriteLine("e) Длина внутреннего пути: " + ext.InternalPathLength());
            Console.WriteLine("   Длина внешнего пути:     " + ext.ExternalPathLength());
        }
    }
}