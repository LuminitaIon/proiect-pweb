using GoSaveMe.Commons.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace GoSaveMe.Commons.Processors
{
    public class MessageQueueProcessor
    {        
        public static bool Publish(string messageType, string username)
        {
            GoSaveMeConfigurationData? config = Configuration.Configuration.Instance.GoSaveMeConfiguration;

            if (config == null)
                return false;

            var factory = new ConnectionFactory()
            {
                HostName = config.RabbitMqIP,
                Port = config.RabbitMqPort,
                UserName = config.RabbitMqUser,
                Password = config.RabbitMqPassword,
                VirtualHost = config.RabbitMqVHost
            };
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: config.RabbitMqQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = $"{messageType}[SEP_TAG]{username}";

                byte[] body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: config.RabbitMqQueue, basicProperties: null, body: body);
            }

            return true;
        }
    }
}
