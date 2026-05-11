namespace Laba2_ball
{
    internal class Program
    {
        static long[] memo;

        static long CountRoutes(int step)
        {
            if (step <= 0) return 1;

            if (memo[step] != 0) return memo[step];

            memo[step] = CountRoutes(step - 1)
                       + CountRoutes(step - 2)
                       + CountRoutes(step - 3);

            return memo[step];
        }

        static void Solve(int n)
        {
            if (n < 1 || n > 30)
            {
                Console.WriteLine("Ошибка: N должно быть от 1 до 30.");
                return;
            }

            memo = new long[n + 1];
            long result = CountRoutes(n);

            Console.WriteLine($"N = {n,-2}  |  маршрутов: {result}");
        }

        static void RunTests(int[] tests)
        {
            foreach (int n in tests)
                Solve(n);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Маршруты мячика по лесенке");
            Console.WriteLine();

            int[] tests1 = { 1, 2, 3, 4, 5 };
            int[] tests2 = { 6, 7, 8, 9, 10 };
            int[] tests3 = { 15, 20, 25, 30 };

            RunTests(tests1);
            RunTests(tests2);
            RunTests(tests3);
        }
    }
}