/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        readonly static Random random = new Random();
        private static readonly int rndmIntArrayLength = 10;
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();
            int[] rndmIntArray = new int[rndmIntArrayLength];
            Task<int[]> task1 = Task.Run(() => FirstTask(rndmIntArray));
            Task<int[]> task2 = task1.ContinueWith((task1Result) => SecondTask(task1Result));
            Task<int[]> task3 = Task.Run(() => task2.ContinueWith(task2Result => ThirdTask(task2Result)));
            Task task4 = Task.Run(() => task3.ContinueWith(task3Result => ForthTask(task3Result)));
            task4.Wait();
            Console.ReadLine();
        }

        private static void ForthTask(Task<int[]> task3Result)
        {
            var rndmIntArray = task3Result.Result;
            Console.WriteLine();
            Console.WriteLine("ForthTask is running");
            double avg = rndmIntArray.Sum() / rndmIntArrayLength;
            Console.WriteLine("Average = " + avg);
            Console.WriteLine("ForthTask was run");
        }

        private static int[] ThirdTask(Task<int[]> task2Result)
        {
            var rndmIntArray = task2Result.Result;
            Console.WriteLine();
            Console.WriteLine("ThirdTask is running");
            Array.Sort(rndmIntArray);
            for (int i = 0; i < rndmIntArrayLength; i++)
            {
                Console.WriteLine(rndmIntArray[i]);
            }
            Console.WriteLine("ThirdTask was run");
            return rndmIntArray;
        }

        private static int[] SecondTask(Task<int[]> task1Result)
        {
            var rndmIntArray = task1Result.Result;
            Console.WriteLine();
            Console.WriteLine("SecondTask is running");
            int rndmNumber = random.Next(1, 20);
            for (int i = 0; i < rndmIntArrayLength; i++)
            {
                rndmIntArray[i] = rndmIntArray[i] * rndmNumber;
                Console.WriteLine(rndmIntArray[i]);
            }
            Console.WriteLine("SecondTask was run");
            return rndmIntArray;
        }

        private static int[] FirstTask(int[] rndmIntArray)
        {
            Console.WriteLine("FirstTask is running");
            for (int i = 0; i < rndmIntArrayLength; i++)
            {
                rndmIntArray[i] = random.Next(1, 20);
                Console.WriteLine(rndmIntArray[i]);
            }
            Console.WriteLine("FirstTask was run");
            return rndmIntArray;
        }
    }
}
