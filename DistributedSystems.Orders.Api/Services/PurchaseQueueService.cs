using DistributedSystems.Orders.Api.Models;
using Microsoft.Extensions.Configuration;
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

        public const string OrdersQueue = "orders";
        public const string OrdersExchange = "";
        public OrdersQueueService(IConfiguration configuration)
        {
            var rabbitMQConfig = configuration.GetSection("RabbitMQConfig")
                .Get<RabbitMQConfig>();

            connectionFactory = new ConnectionFactory
            {
                UserName = rabbitMQConfig.UserName,
                Password = rabbitMQConfig.Password,
                HostName = rabbitMQConfig.HostName,
                Port = rabbitMQConfig.Port
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

    class RabbitMQConfig
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
    }
}
