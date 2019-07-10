using DistributedSystems.Orders.Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystems.Orders.Api.Services
{
    public class OrdersQueueService
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly DefaultContractResolver contractResolver;

        public const string OrdersQueue = "orders";
        public const string OrdersExchange = "";
        public OrdersQueueService()
        {
            connectionFactory = new ConnectionFactory
            {
                UserName = "admin",
                Password = "admin",
                HostName = "localhost",
                Port = 5672
            };
            contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };
        }

        public void PublishNewOrder(Order order)
        {
            using (var conn = connectionFactory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.QueueDeclare(
                        queue: OrdersQueue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );


                    var body = Encoding.UTF8.GetBytes(order.Id.ToString());

                    channel.BasicPublish(exchange: OrdersExchange,
                        routingKey: OrdersQueue,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}
