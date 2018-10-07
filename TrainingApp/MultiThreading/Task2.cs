using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Task2
    {
        private static int[] taskArray;
        public static void RunTask()
        {
            Task.Factory.StartNew(CreateArray).ContinueWith(MultiplyArray).ContinueWith(SortArray).ContinueWith(CalcAverage).Wait();
            Console.ReadKey();
        }


        static void CreateArray()
        {
            Random randNum = new Random();
            taskArray = Enumerable
                .Repeat(0, 10)
                .Select(i => randNum.Next(0, 100))
                .ToArray();
            Array.ForEach(taskArray, Console.WriteLine);
        }
        static void MultiplyArray(object state)
        {
            int randNum = new Random().Next(0, 100);
            taskArray = (taskArray).Select(num => num * randNum).ToArray();
            Console.WriteLine($"The array is multiplied by {randNum}");
            Array.ForEach(taskArray, Console.WriteLine);
        }
        static void SortArray(object state)
        {
            taskArray = taskArray.OrderBy(x => x).ToArray();
            Console.WriteLine("The array is sorted by ascending");
            Array.ForEach(taskArray, Console.WriteLine);
        }
        static void CalcAverage(object state)
        {
            double averageValue = taskArray.Average();
            Console.WriteLine($"Average value = {averageValue}");
        }
    }
}
