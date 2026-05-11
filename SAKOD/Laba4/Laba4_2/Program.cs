namespace Laba4_2
{
    class Node
    {
        public string Value;

        public Node Left;

        public Node Right;

        public Node(string value)
        {
            Value = value;
        }
    }

    class Program
    {
        static bool IsOperator(string value)
        {
            return value == "+" || value == "-" || value == "*" || value == "/";
        }

        static double Calculate(Node node)
        {
            if (node == null)
                return 0;

            if (!IsOperator(node.Value))
                return double.Parse(node.Value);

            double leftValue = Calculate(node.Left);

            double rightValue = Calculate(node.Right);

            switch (node.Value)
            {
                case "+":
                    return leftValue + rightValue;

                case "-":
                    return leftValue - rightValue;

                case "*":
                    return leftValue * rightValue;

                case "/":
                    if (rightValue == 0)
                        throw new DivideByZeroException("Деление на ноль невозможно.");

                    return leftValue / rightValue;

                default:
                    throw new Exception("Неизвестная операция.");
            }
        }

        static void PrintPostfix(Node node)
        {
            if (node == null)
                return;

            PrintPostfix(node.Left);

            PrintPostfix(node.Right);


            Console.Write(node.Value + " ");
        }

        static void Main(string[] args)
        {
            Node root = new Node("*");

            root.Left = new Node("+");
            root.Right = new Node("-");

            root.Left.Left = new Node("2");
            root.Left.Right = new Node("3");

            root.Right.Left = new Node("4");
            root.Right.Right = new Node("1");

            Console.WriteLine("Арифметическое выражение:");
            Console.WriteLine("((2 + 3) * (4 - 1))");

            Console.WriteLine("\nПостфиксный обход:");
            PrintPostfix(root);

            double result = Calculate(root);

            Console.WriteLine("\n\nРезультат вычисления:");
            Console.WriteLine(result);
        }
    }
}
