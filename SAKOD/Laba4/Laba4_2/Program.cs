using System;

namespace Laba4
{
    class ExprNode
    {
        public char Op;
        public double Value;
        public ExprNode Left;
        public ExprNode Right;

        public ExprNode(double value)
        {
            Value = value;
        }

        public ExprNode(char op, ExprNode left, ExprNode right)
        {
            Op = op;
            Left = left;
            Right = right;
        }
    }

    class ExprTree
    {
        private ExprNode Root;


        public ExprTree(ExprNode root)
        {
            Root = root;
        }

        public double Evaluate()
        {
            return Eval(Root);
        }

        public string Postfix()
        {
            return PostfixOf(Root);
        }


        private static double Eval(ExprNode node)
        {
            if (node.Left == null && node.Right == null)
                return node.Value;

            double left = Eval(node.Left);
            double right = Eval(node.Right);

            if (node.Op == '+') return left + right;
            if (node.Op == '-') return left - right;
            if (node.Op == '*') return left * right;
            if (node.Op == '/') return left / right;

            throw new Exception("Неизвестная операция: " + node.Op);
        }

        private static string PostfixOf(ExprNode node)
        {
            if (node.Left == null && node.Right == null)
                return node.Value + " ";

            return PostfixOf(node.Left) + PostfixOf(node.Right) + node.Op + " ";
        }
    }

    class Program
    {
        static void Main()
        {
            ExprNode root =
                new ExprNode('-',
                    new ExprNode('*',
                        new ExprNode('+', new ExprNode(3), new ExprNode(4)),
                        new ExprNode(2)),
                    new ExprNode('/', new ExprNode(10), new ExprNode(5)));

            ExprTree expr = new ExprTree(root);

            Console.WriteLine("Постфиксная запись: " + expr.Postfix());
            Console.WriteLine("Результат: " + expr.Evaluate());
        }
    }
}