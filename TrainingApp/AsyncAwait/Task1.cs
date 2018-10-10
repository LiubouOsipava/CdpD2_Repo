using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Task1
    {
        static void Main(string[] args)
        {
            while (true)
            {
                CancellationTokenSource cts = new CancellationTokenSource();
                DoIterationAsync(cts);
                Console.WriteLine("Enter Y to cancel the operation or any key to proceed...");
                string s = Console.ReadLine();
                if (s == "Y")
                {
                    cts.Cancel();
                }
                else
                {
                    Console.WriteLine("Waiting for total");
                }
            }
        }
        

        
        private static async Task DoIterationAsync(CancellationTokenSource cts)
        {
            
            CancellationToken token = cts.Token;
            Console.WriteLine("Enter integer N for counting sum from 0 to N...");
            int limitNumber = Convert.ToInt32(Console.ReadLine());
            Task<int> countSum = CountSumAsync(limitNumber, token);
            int total = await countSum;
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"The {nameof(CountSumAsync)} is aborted!");
            }
            else
            {
                PrintTotal(total);
            }
        
            
        }

        private static void PrintTotal(object sum)
        {
            Console.WriteLine($"Total: {Convert.ToInt32(sum)}");
        }
        
        private static Task<int> CountSumAsync(int limitNum,CancellationToken token)
        {
                return Task.Run(()=>
                {
                    Console.WriteLine($"The {nameof(CountSumAsync)} is started");
                    int sum = 0;
                    for (int i = 0; i <= limitNum; i++)
                    {
                        if (token.IsCancellationRequested)
                        {
                            Console.WriteLine($"The {nameof(CountSumAsync)} is aborted!");
                            return sum;
                        }
                        Thread.Sleep(2000);
                        sum = sum + i;
                    }
                    return sum;
                });
                
        }
    }
}
