using MongoDb.Models;
using MongoDb.repository;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace DataReceiver
{
    public class Consumer
    {
        readonly IRepository<Book> _repository = new Repository<Book>();
        readonly ConnectionFactory _factory = new ConnectionFactory() { HostName = "localhost" };

        public void Start(CancellationToken token)
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
               queue: "book_queue",
               autoDelete: false,
               exclusive: false,
               durable: false,
               arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += async (sender, e) =>
            {
                var body = e.Body;
                var message = Encoding.UTF8.GetString(body.ToArray());
                var book = JsonSerializer.Deserialize<Book>(message);
                Console.WriteLine(" Received book: \"{0}\"", book.Title);
                await _repository.InsertOneAsync(book);
            };

            channel.BasicConsume(queue: "book_queue",
                                 autoAck: true,
                                 consumer: consumer);

            Console.WriteLine($"Subscribed to the queue 'book_queue'");

            token.WaitHandle.WaitOne();
            Console.WriteLine($"Cancellation is requested");
        }
    }
}
