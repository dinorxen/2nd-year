using System;

namespace laba4_3
{
    class Node
    {
        public int value;
        public Node left;
        public Node right;

        public Node(int value)
        {
            this.value = value;
        }
    }
    class Tree
    {
        private Node root;

        public Tree(Node root)
        {
            this.root = root;
        }

        public int CountLeaves()
        {
            return CountLeaves(root);
        }

        private static int CountLeaves(Node node)
        {
            if (node == null) return 0;
            if (node.left == null && node.right == null) return 1;
            return CountLeaves(node.left) + CountLeaves(node.right);
        }
    }

    class Program
    {
        static void Main()
        {
            Node root = new Node(1);
            root.left = new Node(2);
            root.right = new Node(3);
            root.left.left = new Node(4);
            root.left.right = new Node(5);
            root.right.right = new Node(6);

            Tree tree = new Tree(root);

            Console.WriteLine("Число листьев: " + tree.CountLeaves());
        }
    }
}