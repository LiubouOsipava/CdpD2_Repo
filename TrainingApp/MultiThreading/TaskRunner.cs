using System;
using System.Threading.Tasks;

namespace MultiThreading
{
    class TaskRunner
    {
        static void Main(string[] args)
        {
            Task1.RunTask();
            Task2.RunTask();
            Task3.MultiplyMatrices();
            Task4.RunTask();
            Task5.RunTask();
            Task6.RunTask();
            Task7.RunTask();
        }
        
    }
}
