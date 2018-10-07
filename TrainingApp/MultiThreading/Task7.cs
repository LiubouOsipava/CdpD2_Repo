using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Task7
    {
        public static void RunTask()
        {
            Task.Factory.StartNew(TaskToContinueAnyway).ContinueWith(ContinueAnyway, TaskContinuationOptions.None).Wait();
            Task.Factory.StartNew(TaskToFail).ContinueWith(ContinueOnFail,
                TaskContinuationOptions.OnlyOnFaulted).Wait();
            Task.Factory.StartNew(TaskToFail).ContinueWith(ContinueOnFailAndReuseThread,
                TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously).Wait();
            Task.Factory.StartNew(TaskToCancel, new CancellationToken(true)).ContinueWith(ContinueOutsideThreadPoolOnCancel,
                TaskContinuationOptions.OnlyOnCanceled | TaskContinuationOptions.LongRunning).Wait();

        }

        static void TaskToContinueAnyway()
        {
            Console.WriteLine("The successful task has started...");
        }

        static void TaskToFail()
        {
            Console.WriteLine($"The failure task has started...Thread Id: {Thread.CurrentThread.ManagedThreadId}");
            throw new Exception("Task failed");
        }

        static void TaskToCancel()
        {
            Console.WriteLine($"The cancelled task has started...Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void ContinueAnyway(object state)
        {
            Console.WriteLine($"{nameof(ContinueAnyway)} has executed");
        }

        static void ContinueOnFail(object state)
        {
            Console.WriteLine($"{nameof(ContinueOnFail)} has executed. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void ContinueOnFailAndReuseThread(object state)
        {
            Console.WriteLine($"{nameof(ContinueOnFailAndReuseThread)} has executed. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        static void ContinueOutsideThreadPoolOnCancel(object state)
        {
            Console.WriteLine($"{nameof(ContinueOutsideThreadPoolOnCancel)} has executed. Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
