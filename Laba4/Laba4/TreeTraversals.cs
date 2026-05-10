using System;
using System.Collections.Generic;

namespace Laba4
{
    class TreeTraversals
    {
        public void Preorder(Node root)
        {
            if (root == null)
                return;

            Stack<Node> stack = new Stack<Node>();

            stack.Push(root);

            while (stack.Count > 0)
            {
                Node current = stack.Pop();

                Console.Write(current.Value + " ");

                if (current.Right != null)
                    stack.Push(current.Right);

                if (current.Left != null)
                    stack.Push(current.Left);
            }
        }

        public void Inorder(Node root)
        {
            Stack<Node> stack = new Stack<Node>();
            Node current = root;

            while (current != null || stack.Count > 0)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                current = stack.Pop();

                Console.Write(current.Value + " ");

                current = current.Right;
            }
        }

        public void Postorder(Node root)
        {
            if (root == null)
                return;

            Stack<Node> stack1 = new Stack<Node>();
            Stack<Node> stack2 = new Stack<Node>();

            stack1.Push(root);

            while (stack1.Count > 0)
            {
                Node current = stack1.Pop();

                stack2.Push(current);

                if (current.Left != null)
                    stack1.Push(current.Left);

                if (current.Right != null)
                    stack1.Push(current.Right);
            }

            while (stack2.Count > 0)
            {
                Console.Write(stack2.Pop().Value + " ");
            }
        }

        public void LevelOrder(Node root)
        {
            if (root == null)
                return;

            Queue<Node> queue = new Queue<Node>();

            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();

                Console.Write(current.Value + " ");

                if (current.Left != null)
                    queue.Enqueue(current.Left);

                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
        }
    }
}