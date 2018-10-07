using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Task5
    {
        static Semaphore sem = new Semaphore(10,10);
        static void TreadBody(object state)
        {
            int newState = (int)state;
            newState = newState - 1;
            if (newState > 0)
            {
                sem.WaitOne();
                Console.WriteLine($"State number {newState}; ThreadId: {Thread.CurrentThread.ManagedThreadId}");
                ThreadPool.QueueUserWorkItem(TreadBody, newState);
                sem.Release(1);
            }
        }
        public static void RunTask()
        {
            int state = 10;
            Console.WriteLine($"State number {state}");
            sem.WaitOne();
            ThreadPool.QueueUserWorkItem(TreadBody, state);
            sem.Release(1);
            Console.ReadKey();
        }
    }
}
