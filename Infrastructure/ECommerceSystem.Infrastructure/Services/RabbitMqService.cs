using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore.Metadata;
using ECommerceSystem.Application.Services;

public class RabbitMqSettings
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string QueueName { get; set; }
}

public class RabbitMqService : IMessagingService, IDisposable
{
    private readonly IConnection _connection;
    private readonly RabbitMQ.Client.IModel _channel;

    public RabbitMqService(IOptions<RabbitMqSettings> options)
    {
        var settings = options.Value;
        var factory = new ConnectionFactory()
        {
            HostName = settings.HostName,
            UserName = settings.UserName,
            Password = settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        // Ensure the queue exists. Durable queues persist across restarts.
        _channel.QueueDeclare(queue: settings.QueueName,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    public void Publish<T>(T message, string queueName)
    {
        // Serialize the message to JSON
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        // Publish message to the specified queue
        _channel.BasicPublish(exchange: "",
                              routingKey: queueName,
                              basicProperties: CreateBasicProperties(),
                              body: body);

        Console.WriteLine($"[x] Sent {json}");
    }

    private IBasicProperties CreateBasicProperties()
    {
        var properties = _channel.CreateBasicProperties();
        properties.Persistent = true;
        return properties;
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}
