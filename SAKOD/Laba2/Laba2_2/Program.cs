namespace Laba2_2
{
    internal class Program
    {

        static int FindMax(int[] arr, int left, int right)
        {
            if (left == right)
            {
                return arr[left];
            }

            int mid = (left + right) / 2;
            int max_left = FindMax(arr, left, mid);
            int max_right = FindMax(arr, mid + 1, right);
            return (max_left > max_right) ? max_left : max_right;
        }

        static void Test(int[] arr)
        {
            Console.WriteLine("Массив");
            foreach (int i in arr)
                Console.Write(i + " ");
            Console.WriteLine($"\nМаксимальный элемент: {FindMax(arr, 0, arr.Length-1)}");
            Console.WriteLine("-------------------------------------");
        }

        static void Main(string[] args)
        {
            int[] arr1 = { 3, 8, 1, 6, 2, 9, 4 };
            int[] arr2 = { -5, -2, -11, -1, -7 };
            int[] arr3 = { 12, 12, 7, 5, 12, 3 };
            int[] arr4 = { 25 };

            Test(arr1);
            Test(arr2);
            Test(arr3);
            Test(arr4);
        }
    }
}
