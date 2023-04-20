/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static readonly int threadsCount = 10;
        private readonly static Semaphore semaphore = new Semaphore(1, 1);
        private readonly static Random random = new Random();
        private static int currentThreadNumber = 1;
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();

            Thread thread = new Thread(CreateRecursivelyThread);
            thread.Start(random.Next(10, 20));
            thread.Join();
            Console.ReadLine();
        }
        static void CreateRecursivelyThread(object state)
        {
            int stateValue = (int)state;
            var result = DoWork(stateValue);
            if (currentThreadNumber <= threadsCount)
            {
                Thread thread = new Thread(CreateRecursivelyThread);
                currentThreadNumber++;
                thread.Start(currentThreadNumber);
            }
            Console.WriteLine();
            Console.WriteLine("CreateRecursivelyThread method is finished for thread: {0}", currentThreadNumber);
            // to do, print after DoWork() the last message
        }

        private static int DoWork(int stateValue)
        {
            semaphore.WaitOne();

            Console.WriteLine("Thread {0} has entered the semaphore", currentThreadNumber);
            stateValue--;
            Console.WriteLine("State value is : {0} ", stateValue);

            Console.WriteLine("Thread {0} is releasing the semaphore", currentThreadNumber);
            Console.WriteLine();

            semaphore.Release();

            return stateValue;

        }
    }
}
