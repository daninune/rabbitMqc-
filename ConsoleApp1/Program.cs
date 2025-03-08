using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client.Events;

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


            /*var factory1 = new RabbitMQ.Client.ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672/vhost")
            };*/
    //        var connection1 = await factory1.CreateConnectionAsync();
            var connection = await factory.CreateConnectionAsync();
             //var channel = await connection.CreateChannelAsync();

      //      var channel1 = await connection1.CreateChannelAsync();
            // Create a connection to the RabbitMQ server
            /*IConnection connection = factory.CreateConnectionAsync().Result;

            // Create a channel
            */
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


            

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, eventArgs) =>
            {
                Console.WriteLine("Doing a fake API call...");
                await Task.Delay(2000);
                Console.WriteLine("Done with fake API call!");
                object p = channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            };
            channel.BasicConsumeAsync("myQueue", false, consumer);
        }
        }
    }

