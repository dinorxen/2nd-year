namespace laba6
{
    class HashItem
    {
        private int?[] table;
        public HashItem(int size)
        {
            this.table = new int?[size];
        }

        private int HashFunc(int key)
        {
            return Math.Abs(key) % table.Length;
        }

        public void Insert(int key)
        {
            int index = HashFunc(key);
            int probes = 0;

            while (table[index] != null && probes < table.Length)
            {
                index = (index + 1) % table.Length;
                probes++;
            }

            if (probes < table.Length)
            {
                table[index] = key;
            }
        }

        public int CountProbesForUnsuccessfulSearch(int key)
        {
            int index = HashFunc(key);
            int probes = 1;

            while (probes < table.Length && table[index] != null)
            {
                index = (index + 1) % table.Length;
                probes++;
            }

            return probes;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rd = new Random();
            int[] test = { 5000, 10000, 20000 };
            foreach (int n in test)
            {
                HashItem hashtable = new HashItem(n);
                for (int i = 0; i < n / 2; i++)
                {
                    int value = rd.Next(1, 1000000);
                    hashtable.Insert(value);
                }

                int searchCount = 10000;
                int total = 0;

                for (int i = 0; i < searchCount; i++)
                {
                    int value = rd.Next(1, 1000000);
                    total += hashtable.CountProbesForUnsuccessfulSearch(value);
                }

                Console.WriteLine($"Размер таблицы: {n}");
                Console.WriteLine($"Количество вставленных элементов: {n / 2}");
                Console.WriteLine($"Среднее количество проб при неудачном поиске: {(double)total / searchCount}");
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
