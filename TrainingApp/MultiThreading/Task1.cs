using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Task1
    {
        public static void RunTask()
        {
            Task[] tasksArray = new Task[100];
            for (int taskNumber = 1; taskNumber <= 100; taskNumber++)
            {
                tasksArray[taskNumber - 1] = Task.Factory.StartNew(TaskWork, taskNumber);
            }

            Task.WaitAll(tasksArray);
            Console.ReadKey();
        }

        static void TaskWork(object taskNumber)
        {
            for (int iterationNumber = 1; iterationNumber <= 1000; iterationNumber++)
            {
                Console.WriteLine($"Task {taskNumber} - {(int)iterationNumber}");
            }
        }
    }
}
