using System;
using System.Diagnostics;

namespace Lab1_Complexity
{
    class Program
    {
        // Варіант 1: бінарний пошук O(log n) у відсортованому масиві
        static int BinarySearch(int[] arr, int target)
        {
            int lo = 0, hi = arr.Length - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                if (arr[mid] == target) return mid;
                if (arr[mid] < target) lo = mid + 1; else hi = mid - 1;
            }
            return -1;
        }

        static long LinearTimeOperation(int[] arr)
        {
            long sum = 0;
            for (int i = 0; i < arr.Length; i++) sum += arr[i];
            return sum;
        }

        static void Main()
        {
            int[] sizes = { 1_000, 10_000, 100_000 };
            var rand = new Random(42);
            Console.WriteLine("N         | O(n) мс      | O(log n) мс");
            foreach (int n in sizes)
            {
                int[] data = new int[n];
                for (int i = 0; i < n; i++) data[i] = rand.Next(1, 1000);
                Array.Sort(data); // для бінарного пошуку масив має бути відсортований

                var sw = Stopwatch.StartNew();
                long sum = LinearTimeOperation(data);
                sw.Stop();
                double tLin = sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                int idx = 0;
                for (int k = 0; k < 1000; k++) idx = BinarySearch(data, data[n / 3]); // 1000 повторів для точності
                sw.Stop();
                double tBin = sw.Elapsed.TotalMilliseconds / 1000;

                Console.WriteLine($"{n,-9} | {tLin,-12:F6} | {tBin:F6} (idx={idx}, sum={sum})");
            }
        }
    }
}
