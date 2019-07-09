using DistributedSystems.Project.Purchase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributedSystems.Project.Purchase.Services
{
    public class OrdersQueueService
    {

        private readonly ConnectionFactory connectionFactory;
        private readonly DefaultContractResolver contractResolver;

        public const string OrdersQueue = "orders";

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

                    var jsonPayload = JsonConvert.SerializeObject(order, new JsonSerializerSettings
                    {
                        ContractResolver = contractResolver
                    });

                    var body = Encoding.UTF8.GetBytes(jsonPayload);

                    channel.BasicPublish(exchange: "",
                        routingKey: OrdersQueue,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}
