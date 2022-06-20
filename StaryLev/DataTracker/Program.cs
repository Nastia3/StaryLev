using System;
using System.Threading.Tasks;

namespace DataTracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Publisher publisher = new Publisher();
            await publisher.StartAsync(56);

            Console.ReadLine();
        }
    }
}
