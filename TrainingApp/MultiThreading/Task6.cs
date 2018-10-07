using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiThreading
{
    static class Task6
    {
        static List<int> _sharedCollection = new List<int>();
        static void CreateCollection()
        {
            Random rand = new Random();
            _sharedCollection = Enumerable
                .Repeat(0, 20)
                .Select(i => rand.Next(0, 100))
                .ToList();
            _sharedCollection.ForEach(Console.WriteLine);
        }

        static void AddElementsSet(object element)
        {
            lock (_sharedCollection)
            {
                _sharedCollection.Add((int)element);
                Console.WriteLine($"Added element {(int)element}");
            }
        }
        static void PrintCollection(object state)
        {
            lock (_sharedCollection)
            {
                _sharedCollection.ForEach(Console.WriteLine);
            }
        }

        public static void RunTask()
        {
            CreateCollection();
            Random rand = new Random();
            var newElements = Enumerable
                .Repeat(0, 10)
                .Select(i => rand.Next(0, 100));
            Parallel.ForEach(newElements,el=> Task.Factory.StartNew(AddElementsSet,el).ContinueWith(PrintCollection).Wait());
            Console.ReadKey();
        }
    }
}
