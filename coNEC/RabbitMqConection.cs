using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conect
{
    public class RabbitMqConection 
    {
        

        public  ConnectionFactory GetConnectionFactory()
        {
            ConnectionFactory factory;

            factory = new ConnectionFactory
            {
                HostName = "localhost",    // Replace with your RabbitMQ server's hostname
                Port = 8080,               // Default RabbitMQ port
                UserName = "guest",        // RabbitMQ username
                Password = "guest"         // RabbitMQ password
            };
            return factory;
        }
            
            

    }
}
