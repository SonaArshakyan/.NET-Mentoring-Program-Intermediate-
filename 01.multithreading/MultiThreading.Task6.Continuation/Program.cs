﻿/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            var cancelSource = new CancellationTokenSource();
            CancellationToken token = cancelSource.Token;

            Task parentTask = Task.Run(() => ParentTask(token), token);
            Task taskA = parentTask.ContinueWith(t => TaskA());
            Task taskB = parentTask.ContinueWith((resultOfParentTask) =>
            {
                if (resultOfParentTask.IsFaulted)
                    TaskB();
            },
            TaskContinuationOptions.OnlyOnFaulted);
            Task taskC = parentTask.ContinueWith((resultOfParentTask) =>
            {
                if (resultOfParentTask.IsFaulted)
                    TaskC();
            },
            TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);

            Task taskD = parentTask.ContinueWith((resultOfParentTask) =>
            {
                if (resultOfParentTask.IsCanceled)
                    TaskD();
            },  TaskContinuationOptions.LongRunning | TaskContinuationOptions.OnlyOnCanceled);

           
           // cancelSource.Cancel();

            try
            {

                Task.WaitAll(parentTask, taskA, taskB, taskC, taskD);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine();
                Console.WriteLine("Parent task was failed!");
                if (ex.InnerException is OperationCanceledException)
                    Console.Write("Task canceled!");
            }
        }


        private static void TaskD()
        {
            Console.WriteLine();
            Console.WriteLine("Thread belongs to managed thread pool? = " + Thread.CurrentThread.IsThreadPoolThread);
            Console.WriteLine("TaskD Continuation task is executed outside of the thread pool when the parent task was cancelled. ThreadId : {0} ", Thread.CurrentThread.ManagedThreadId);
        }

        private static void TaskC()
        {
            Console.WriteLine();
            Console.WriteLine("TaskC Continuation task is executed when the parent task was finished with fail and parent task thread was reused for continuation.  ThreadId : {0} ", Thread.CurrentThread.ManagedThreadId);
        }

        private static void TaskB()
        {
            Console.WriteLine();
            Console.WriteLine("TaskB Continuation task is executed when the parent task finished without success. ThreadId : {0} ", Thread.CurrentThread.ManagedThreadId);
        }

        private static void TaskA()
        {
            Console.WriteLine();
            Console.WriteLine("TaskA Continuation task is executed regardless of the result of the parent task. ThreadId : {0} ", Thread.CurrentThread.ManagedThreadId);
        }

        private static object ParentTask(CancellationToken token)
        {
            Console.WriteLine();
            Console.WriteLine("Parent task is executing!  ThreadId : {0} ", Thread.CurrentThread.ManagedThreadId);
            token.ThrowIfCancellationRequested();
            throw new NullReferenceException();
        }
    }
}
