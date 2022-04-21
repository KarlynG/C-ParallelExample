using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{
    internal class FirstTest
    {
        static void PizzaTask()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int totalPizza = 5;
            Console.WriteLine($"Started preparing {totalPizza} pizza");
            for (var x = 1; x <= totalPizza; x++)
            {
                MakePizza(x);
            }

            stopwatch.Stop();

            Console.WriteLine($"Finished preparing {totalPizza} pizza");
            Console.WriteLine("Elapsed time:" + stopwatch.Elapsed.TotalSeconds);
        }

        static async Task PizzaTaskAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int totalPizza = 5;
            Console.WriteLine($"Started preparing {totalPizza} pizza");
            var tasks = Enumerable.Range(1, totalPizza).Select(i => MakePizzaAsync(i));
            await Task.WhenAll(tasks);

            stopwatch.Stop();

            Console.WriteLine($"Finished preparing {totalPizza} pizza");
            Console.WriteLine("Elapsed time:" + stopwatch.Elapsed.TotalSeconds);
        }

        static void MakePizza(int n)
        {
            PreparePizza(n);
            BakePizza(n);
        }

        static async Task MakePizzaAsync(int n)
        {
            await PreparePizzaAsync(n);
            await BakePizzaAsync(n);
        }

        static void PreparePizza(int n)
        {
            Console.WriteLine("Start preparing pizza " + n);
            Thread.Sleep(1000);
            Console.WriteLine("Finished preparing pizza " + n);
        }

        static void BakePizza(int n)
        {
            Console.WriteLine("Start baking pizza " + n);
            Thread.Sleep(2000);
            Console.WriteLine("Finished baking pizza " + n);
        }
        static async Task BakePizzaAsync(int n)
        {
            Console.WriteLine("Start baking pizza " + n);
            await Task.Delay(2000);
            Console.WriteLine("Finished baking pizza " + n);
        }

        static async Task PreparePizzaAsync(int n)
        {
            Console.WriteLine("Start preparing pizza " + n);
            await Task.Delay(1000);
            Console.WriteLine("Finished preparing pizza " + n);
        }
    }
}
