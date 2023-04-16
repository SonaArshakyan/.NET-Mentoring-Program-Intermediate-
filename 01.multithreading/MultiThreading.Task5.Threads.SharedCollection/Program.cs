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

            Task task1 = Task.Factory.StartNew(() =>
            {
                for (int i = 1; i <= elementsCount; i++)
                {
                    DoWork(i);
                }
            });
            Task task2 = Task.Factory.StartNew( () => 
            {
                for (int i = 1; i <= elementsCount; i++)
                {
                    DoWork(null);
                }
            });

            Task.WaitAll(task1, task2);
        }

        private static void DoWork(object state)
        {
            // Acquire the mutex
            mutex.WaitOne();

            Console.WriteLine();

            if (state != null)
            {
                Console.WriteLine("Thread one Acquired mutex" , Thread.CurrentThread.Name);
                int elementValue = (int)state;
                elementsCollection.Add(elementValue);
                Console.WriteLine("Thread one added an element");
            }
            else
            {
                Console.WriteLine("Thread second Acquired mutex" , Thread.CurrentThread.Name);
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
