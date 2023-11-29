using MailSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

class Program
{
    public static void Main()
    {
        ConfigurationData? configurationData = Configuration.Instance.MailsenderConfiguration;

        if (configurationData == null)
            return;

        MailProcessor mailProcessor = new(
            configurationData.SmtpUsername,
            configurationData.SmtpPassword,
            configurationData.SmtpClient,
            configurationData.SmtpPort
        );

        var factory = new ConnectionFactory()
        {
            HostName = configurationData.RabbitMqIP,
            Port = configurationData.RabbitMqPort,
            UserName = configurationData.RabbitMqUser,
            Password = configurationData.RabbitMqPassword,
            VirtualHost = configurationData.RabbitMqVHost

        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        channel.QueueDeclare(queue: configurationData.RabbitMqQueue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

        Console.WriteLine("[*] Waiting for messages.");

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            string messageType = message.Split("[SEP_TAG]")[0];
            string toEmail = message.Split("[SEP_TAG]")[1];

            switch (messageType)
            {
                case "PROFILE_CREATED":
                    mailProcessor.SendSignUpEmail(toEmail);
                    break;
                case "PROFILE_UPDATED":
                    mailProcessor.SendProfileUpdatedEmail(toEmail);
                    break;
                case "NEWS_POSTED":
                    mailProcessor.SendNewsPostedEmail(toEmail);
                    break;
                case "NEWS_QUEUED":
                    mailProcessor.SendNewsWaitingForApprovalEmail(toEmail);
                    break;
                default:
                    break;
            }

            Console.WriteLine(" [x] Received {0}", message);
        };

        channel.BasicConsume(queue: configurationData.RabbitMqQueue,
                                autoAck: true,
                                consumer: consumer);
        Console.WriteLine("Press [enter] to exit.");
        Console.ReadKey();
    }
}

