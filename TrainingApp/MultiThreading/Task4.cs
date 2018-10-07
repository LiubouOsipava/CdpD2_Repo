using System;
using System.Threading;

namespace MultiThreading
{
    static class Task4
    {
        static void TreadBody(object state)
        {
            int newState = (int) state;
            newState = newState - 1;
            if (newState>0)
            {
                Console.WriteLine($"State number {newState}");
                Thread tread = new Thread(TreadBody);
                tread.Start(newState);
                tread.Join();
            }
        }

        public static void RunTask()
        {
            int state = 10;
            Console.WriteLine($"State number {state}");
            Thread tread = new Thread(TreadBody);
            tread.Start(state);
            tread.Join();
            Console.ReadKey();
        }

    }
}
