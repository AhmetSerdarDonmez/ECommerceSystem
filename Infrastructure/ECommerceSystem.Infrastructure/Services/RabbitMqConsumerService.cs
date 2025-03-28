using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using ECommerceSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using ECommerceSystem.Application.Services; // Add this for EMailModel

public class EMailModel
{
    public string receptor { get; set; } = string.Empty;
    public string subject { get; set; } = string.Empty;
    public string body { get; set; } = string.Empty;
}
public class RabbitMqConsumerService : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly IEMailService _emailService; // Inject email service
    private IConnection _connection;
    private IModel _channel;
   
    public RabbitMqConsumerService(
        IOptions<RabbitMqSettings> options,
        IEMailService emailService) // Add dependency injection
    {
        _settings = options.Value;
        _emailService = emailService;
        InitializeRabbitMqListener();
    }

    private void InitializeRabbitMqListener()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _settings.QueueName, // Use correct queue name
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                // Deserialize message
                var emailModel = JsonSerializer.Deserialize<EMailModel>(message);

                if (emailModel != null)
                {
                    // Send email using injected service
                    await _emailService.SendEmailAsync(
                        emailModel.receptor,
                        emailModel.subject,
                        emailModel.body);
                }
            }
            finally
            {
                _channel.BasicAck(ea.DeliveryTag, false);
            }
        };

        _channel.BasicConsume(
            queue: _settings.QueueName,
            autoAck: false,
            consumer: consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}