using System;
using System.Diagnostics;
using System.Numerics;

namespace Laba2
{
    internal class Fibionachi
    {
        static BigInteger Bine(int n)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double psi = (1 - Math.Sqrt(5)) / 2;
            double f = (Math.Pow(phi, n) - Math.Pow(psi, n)) / Math.Sqrt(5);
            return (BigInteger)Math.Round(f);
        }

        static BigInteger Iterative(int n)
        {
            if (n <= 1)
                return n;

            BigInteger a = 0;
            BigInteger b = 1;

            for (int i = 2; i <= n; i++)
            {
                BigInteger x = a + b;
                a = b;
                b = x;
            }

            return b;
        }

        static (BigInteger, BigInteger) DivideConquer(int n)
        {
            if (n == 0)
                return (0, 1);

            var (a, b) = DivideConquer(n / 2);

            BigInteger c = a * (2 * b - a);
            BigInteger d = a * a + b * b;

            if (n % 2 == 0)
                return (c, d);
            else
                return (d, c + d);
        }

        static BigInteger TopDown(int n, BigInteger[] memo)
        {
            if (n <= 1)
                return n;

            if (memo[n] != -1)
                return memo[n];

            memo[n] = TopDown(n - 1, memo) + TopDown(n - 2, memo);
            return memo[n];
        }

        static BigInteger BottomUp(int n)
        {
            if (n <= 1)
                return n;

            BigInteger[] dp = new BigInteger[n + 1];
            dp[0] = 0;
            dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        static double MeasureTime(Func<BigInteger> method, int repeats = 10000)
        {
            Stopwatch sw = new Stopwatch();
            BigInteger result = 0;

            sw.Start();
            for (int i = 0; i < repeats; i++)
            {
                result = method();
            }
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds / repeats;
        }

        static void Main(string[] args)
        {
            int n = 35;

            Console.WriteLine("Сравнение времени вычисления 35-го числа Фибоначчи");
            Console.WriteLine();

            Console.WriteLine("1. Формула Бине");
            Console.WriteLine("F(35) = " + Bine(n));
            Console.WriteLine(MeasureTime(() => Bine(n)) + " ms");
            Console.WriteLine("----------------------------");

            Console.WriteLine("2. Итерационный метод");
            Console.WriteLine("F(35) = " + Iterative(n));
            Console.WriteLine(MeasureTime(() => Iterative(n)) + " ms");
            Console.WriteLine("----------------------------");

            Console.WriteLine("3. Метод «разделяй и властвуй»");
            Console.WriteLine("F(35) = " + DivideConquer(n).Item1);
            Console.WriteLine(MeasureTime(() => DivideConquer(n).Item1) + " ms");
            Console.WriteLine();

            Console.WriteLine("Нахождение точного ответа за O(logN) 175-го числа Фибоначчи");
            Console.WriteLine("F(175) = " + DivideConquer(175).Item1);
            Console.WriteLine(MeasureTime(() => DivideConquer(175).Item1) + " ms");
            Console.WriteLine("----------------------------");

            Console.WriteLine("4. Метод нисходящего динамического программирования");
            BigInteger[] memo = new BigInteger[n + 1];
            for (int i = 0; i < memo.Length; i++)
                memo[i] = -1;

            Console.WriteLine("F(35) = " + TopDown(n, memo));

            Console.WriteLine(MeasureTime(() =>
            {
                BigInteger[] localMemo = new BigInteger[n + 1];
                for (int i = 0; i < localMemo.Length; i++)
                    localMemo[i] = -1;
                return TopDown(n, localMemo);
            }) + " ms");
            Console.WriteLine("----------------------------");

            Console.WriteLine("5. Метод восходящего динамического программирования");
            Console.WriteLine("F(35) = " + BottomUp(n));
            Console.WriteLine(MeasureTime(() => BottomUp(n)) + " ms");
        }
    }
}