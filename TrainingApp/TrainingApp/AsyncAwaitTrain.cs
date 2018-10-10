using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrainingApp
{
    class AsyncAwaitTrain
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(CallBack, i);
            }

            Console.ReadKey();
            Thread.Sleep(1000);

            for (int i = 0; i < 10; i++)
            {
                Task.Factory.StartNew(CallBack, i);
            }
            Console.ReadKey();
        }

        static void CallBack(object state)
        {
            Console.WriteLine($"Iteration: {(int)state}, Thread ID: {Thread.CurrentThread.ManagedThreadId}");
        }
    }
}
