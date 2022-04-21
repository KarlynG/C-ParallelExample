using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTest
{
    class Program
    {
        public static HttpClient client = new HttpClient();
        public static int NumberOfIterations { get; } = 10;
        static void Main(string[] args)
        {
            CallPokemonsNotParallel().GetAwaiter().GetResult();
            Console.WriteLine("\n\n\n");
            CallPokemonsAsync().GetAwaiter().GetResult();
        }
        private static async Task CallPokemonsNotParallel()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 1; i < NumberOfIterations; i++)
            {
                var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + i);
                var response = JsonSerializer.Deserialize<Pokemon>(await result.Content.ReadAsStringAsync());
                Console.WriteLine($"Pokemon #{i}: {response.name}");
            }
            stopwatch.Stop();
            Console.WriteLine("Elapsed time:" + stopwatch.Elapsed.TotalSeconds);
        }
        private static async Task CallPokemonsAsync()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var tasks = Enumerable.Range(1, NumberOfIterations).Select(i => CallPokemons(i));
            await Task.WhenAll(tasks);
            stopwatch.Stop();
            Console.WriteLine("Elapsed time:" + stopwatch.Elapsed.TotalSeconds);
        }
        private static async Task CallPokemons(int i)
        {
            var result = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/" + i);
            var response = JsonSerializer.Deserialize<Pokemon>(await result.Content.ReadAsStringAsync());
            Console.WriteLine($"Pokemon #{i}: {response.name}");
        }
    }
    class Pokemon
    {
        public string name { get; set; }
    }
}
