using System;
using System.Threading;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 26: Demonstrate Race Conditions with Multi-threading
    /// </summary>
    public static class Exercise26
    {
        private static int sharedCounterUnsafe = 0;
        private static int sharedCounterSafe = 0;
        private static readonly object lockObj = new object();

        private static void IncrementUnsafe()
        {
            for (int i = 0; i < 100000; i++)
            {
                sharedCounterUnsafe++; // NOT thread-safe - subject to race conditions
            }
        }

        private static void IncrementSafe()
        {
            for (int i = 0; i < 100000; i++)
            {
                lock (lockObj)
                {
                    sharedCounterSafe++; // thread-safe with lock
                }
            }
        }

        public static void Run()
        {
            const int threadCount = 4;

            // Unsafe version - demonstrates race condition
            Thread[] unsafeThreads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                unsafeThreads[i] = new Thread(IncrementUnsafe);
                unsafeThreads[i].Start();
            }
            foreach (var t in unsafeThreads) t.Join();

            Console.WriteLine($"Expected counter value: {threadCount * 100000}");
            Console.WriteLine($"Unsafe counter value:   {sharedCounterUnsafe} (likely LESS than expected due to race condition)");

            // Safe version - using lock
            Thread[] safeThreads = new Thread[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                safeThreads[i] = new Thread(IncrementSafe);
                safeThreads[i].Start();
            }
            foreach (var t in safeThreads) t.Join();

            Console.WriteLine($"Safe counter value:     {sharedCounterSafe} (matches expected, thanks to 'lock')");
        }
    }
}
