namespace Laba2_Hanoi
{
    internal class Program
    {
        static int moveCount = 0;

        static void Hanoi(int n, char from, char to, char aux)
        {
            if (n == 1)
            {
                moveCount++;
                Console.WriteLine($"  Ход {moveCount,2}: диск 1  [{from}] → [{to}]");
                return;
            }

            Hanoi(n - 1, from, aux, to);

            moveCount++;
            Console.WriteLine($"  Ход {moveCount,2}: диск {n}  [{from}] → [{to}]");

            Hanoi(n - 1, aux, to, from);
        }
        
        static void Solve(int n)
        {
            moveCount = 0;
            Console.WriteLine($"Ханойская башня: n = {n,-2}");
            Console.WriteLine($"Минимум ходов: 2^{n} - 1 = {(int)Math.Pow(2, n) - 1}");
            Console.WriteLine();
            Hanoi(n, 'A', 'C', 'B');
            Console.WriteLine();
            Console.WriteLine($"Итого ходов: {moveCount}");
            Console.WriteLine("------------------------------------");
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            Solve(1);
            Solve(2);
            Solve(3);
            Solve(4);
        }
    }
}
