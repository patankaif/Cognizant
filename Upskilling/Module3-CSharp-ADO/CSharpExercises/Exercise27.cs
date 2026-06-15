using System;
using System.Threading;

namespace CSharpExercises
{
    /// <summary>
    /// Exercise 27: Simulate and Resolve a Deadlock
    /// </summary>
    public static class Exercise27
    {
        private static readonly object lockA = new object();
        private static readonly object lockB = new object();

        // This method, if both threads run it with swapped lock order and
        // use Monitor.Enter (blocking) instead of TryEnter, can deadlock:
        //   Thread 1: locks A, then waits for B
        //   Thread 2: locks B, then waits for A
        private static void SafeAcquireLocks(string threadName, object first, object second)
        {
            bool gotFirst = false, gotSecond = false;
            try
            {
                gotFirst = Monitor.TryEnter(first, TimeSpan.FromMilliseconds(500));
                if (gotFirst)
                {
                    Console.WriteLine($"{threadName}: acquired first lock.");
                    Thread.Sleep(100); // simulate work, increases chance of contention

                    gotSecond = Monitor.TryEnter(second, TimeSpan.FromMilliseconds(500));
                    if (gotSecond)
                    {
                        Console.WriteLine($"{threadName}: acquired second lock. Doing work...");
                    }
                    else
                    {
                        Console.WriteLine($"{threadName}: could not acquire second lock - backing off to avoid deadlock.");
                    }
                }
                else
                {
                    Console.WriteLine($"{threadName}: could not acquire first lock - backing off.");
                }
            }
            finally
            {
                if (gotSecond) Monitor.Exit(second);
                if (gotFirst) Monitor.Exit(first);
            }
        }

        public static void Run()
        {
            Console.WriteLine("Discussion: A classic deadlock occurs when Thread 1 locks A then " +
                               "waits for B, while Thread 2 locks B then waits for A - neither can proceed.");
            Console.WriteLine("Resolution demonstrated below using Monitor.TryEnter with a timeout, " +
                               "which avoids indefinite blocking by backing off if a lock isn't available.\n");

            // Both threads request locks in opposite order, which would deadlock with Monitor.Enter,
            // but TryEnter with a timeout prevents indefinite waiting.
            Thread t1 = new Thread(() => SafeAcquireLocks("Thread 1", lockA, lockB));
            Thread t2 = new Thread(() => SafeAcquireLocks("Thread 2", lockB, lockA));

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("\nBoth threads completed without deadlocking.");
        }
    }
}
