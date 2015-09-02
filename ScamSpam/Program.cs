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
        static void Main(string[] args)
        {
            Console.WriteLine("ScamSpam");
            Console.WriteLine("Flood SMS scam links with spam");

            var urlFormat = "http://www.a3c.be/{0}/{1}.php?m=";

            var urlList = new List<string>();

            for (int i = 0; i < 10; i++)
            {
                for (char j = 'a'; j <= 'z'; j++)
                {
                    // prevent unsuccessful spam attempts
                    // currently we know that 5, v and m work as a combination
                    if (i == 5 && (j == 'v' || j == 'm'))
                        urlList.Add(string.Format(urlFormat, i, j));
                }
            }

            var junkData = generateJunk();

            var client = new HttpClient();
            var spamUrls = new List<string>();

            Parallel.ForEach(junkData, (data) =>
            {
                foreach (var url in urlList)
                {
                    Console.WriteLine("spamming {0}{1}", url, data);
                    var getTask = client.GetAsync(url + data.ToString());
                    getTask.Wait();

                    if (!getTask.Result.IsSuccessStatusCode)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("unsuccessful spam attempt {0}", data);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            });

            Console.ReadKey();
        }

        private static List<int> generateJunk()
        {
            var results = new List<int>();

            // need a better way to generate all the numbers
            var startNumber = 790000001;

            while (startNumber < 800000000)
                results.Add(++startNumber);

            return results;
        }
    }
}
