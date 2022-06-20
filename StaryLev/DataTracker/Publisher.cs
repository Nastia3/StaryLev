using DataTracker.Selenium;
using MongoDb.Models;
using MongoDb.repository;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DataTracker
{
    public class Publisher
    {
        readonly ConnectionFactory _factory = new ConnectionFactory() { HostName = "localhost" };
        readonly IRepository<Book> _repository = new Repository<Book>();
        readonly StaryLev gidOnline = new StaryLev();
        List<string> bookTitles = new List<string>();

        private void Publicsh(Book book)
        {
            string message = JsonSerializer.Serialize(book);

            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "book_queue", 
                autoDelete: false, 
                exclusive:false, 
                durable: false,
                arguments: null);
            

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                routingKey: "book_queue",
                                basicProperties: null,
                                body: body);

            Console.WriteLine($"Message type [book]: \"{book.Title}\" is sent into Direct Exchange.");

        }

        public async Task StartAsync(int recheckingNumber)
        {
            bookTitles = (await _repository.GetAllAsync()).Select(m => m.Title).ToList();
            await foreach (var book in gidOnline.GetBooksAsync(recheckingNumber))
            {
                if (!bookTitles.Contains(book.Title))
                {
                    Console.WriteLine($"Get new book: \"{book.Title}\"");
                    bookTitles.Add(book.Title);
                    Publicsh(book);
                }
                else
                {
                    Console.WriteLine($"Get old book: \"{book.Title}\"");
                }
            }
            Timer timer = new Timer(new TimerCallback(Track), null, 5000, 60000);      
        }

        public void Track(object obj)
        {
            Task.Run(async () =>
            {
                Console.WriteLine("Start Tracking");
                await foreach (var book in gidOnline.GetBooksAsync(28))
                {
                    if (!bookTitles.Contains(book.Title))
                    {
                        Console.WriteLine($"Get new book: \"{book.Title}\"");
                        bookTitles.Add(book.Title);
                        Publicsh(book);
                    }
                    else
                    {
                        Console.WriteLine($"Get old book: \"{book.Title}\"");
                    }
                }
                Console.WriteLine("End Tracking");
            });
        }
    }
}
