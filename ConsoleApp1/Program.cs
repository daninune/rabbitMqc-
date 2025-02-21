using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {


            // RabbitMQ server connection parameters
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",    // Replace with your RabbitMQ server's hostname
                Port = 5672,               // Default RabbitMQ port
                UserName = "guest",        // RabbitMQ username
                Password = "guest"         // RabbitMQ password
            };

            // Create a connection to the RabbitMQ server
            IConnection connection = factory.CreateConnectionAsync().Result;

            // Create a channel
            
            IChannel channel = connection.CreateChannelAsync().Result;

            // Declare a queue
            string queueName = "myQueue";  // Replace with your queue name
            channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
    arguments: null);

            // Message to send
            string message = "Hello, RabbitMQ!";
            byte[] body = Encoding.UTF8.GetBytes(message);

            // Publish the message to the queue


            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);
            Console.WriteLine($"Sent: {message}");
        }
    }
}
