/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private readonly static int elementsCount = 10;
        private readonly static List<int> elementsCollection = new List<int>();
        static Mutex mutex = new Mutex();

        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            for (int i = 1; i <= elementsCount; i++)
            {
                ThreadPool.QueueUserWorkItem(DoWork, i);
                Task task = Task.Factory.StartNew(DoWork, null);
                task.Wait();
            }
            Task.WaitAll();
        }

        private static void DoWork(object state)
        {
            // Acquire the mutex
            mutex.WaitOne();

            Console.WriteLine();

            if (state != null)
            {
                Console.WriteLine("Thread one Acquired mutex");
                int elementValue = (int)state;
                elementsCollection.Add(elementValue);
                Console.WriteLine("Thread one added an element");
            }
            else
            {
                Console.WriteLine("Thread second Acquired mutex");
                foreach (var element in elementsCollection)
                {
                    Console.Write(element);
                }
            }

            mutex.ReleaseMutex();
            // Release the mutex
            Console.WriteLine();
        }
    }
}
