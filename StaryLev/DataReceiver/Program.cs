using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            Consumer consumer = new Consumer();
            Task.Run(()=> consumer.Start(token));
            
            Console.ReadLine();
            cts.Cancel();
        }
    }
}
