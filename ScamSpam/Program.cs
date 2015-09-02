using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ScamSpam
{
    class Program
    {
        private static List<int> prefixes = new List<int>() {
            71,
            72,
            73,
            74,
            76,
            78,
            79,
            81,
            82,
            83,
            84,
            85,
            86
        };

        static void Main(string[] args)
        {
            Console.WriteLine("ScamSpam");
            Console.WriteLine("Flood SMS scam links with spam");
            Console.WriteLine();
            Console.Write("Url\t:");
            var url = Console.ReadLine();

            var client = new HttpClient();
            var random = new Random();

            while (!Console.KeyAvailable)
            {
                var prefix = prefixes.Skip(random.Next(0, prefixes.Count() - 1)).First() * 10000000;
                var payload = random.Next(0, 999999);

                payload += prefix;

                Console.WriteLine("spamming {0}{1}", url, payload.ToString("000000000"));
                var getTask = client.GetAsync(url + payload.ToString("000000000"));
                getTask.Wait();

                if (!getTask.Result.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("unsuccessful spam attempt {0}", payload.ToString("000000000"));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }

            Console.ReadKey();
        }
    }
}
