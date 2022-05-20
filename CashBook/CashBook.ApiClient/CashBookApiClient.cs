using System.Net.Http.Json;
using System.Text;
using CashBook.ApiClient;
using CashBook.ApiClient.Interface;
using CashBook.Application.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace CashBook.ApiClient;

public class CashBookApiClient : ICashBookApiClient
{
	private readonly HttpClient _http;
	private readonly CashBankOptions _options;
	private readonly ILogger<CashBookApiClient> _logger;

	public CashBookApiClient(
		IOptions<CashBankOptions> options,
		ILogger<CashBookApiClient> logger,
		HttpClient http)
	{
		_logger = logger;
		_http = http;
		_options = options.Value;
	}

	public void SendToRabbit(CashBookDto obj)
	{
		var factory = new ConnectionFactory
		{
			Uri = new Uri("amqp://guest:guest@localhost:5672")
		};
		using var connection = factory.CreateConnection();
		using var channel = connection.CreateModel();
		channel.QueueDeclare("demo-queue",
			durable: true,
			exclusive: false,
			autoDelete: false,
			arguments: null);

		var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
		_logger.LogInformation("Send to CashBook to Add {obj}", obj);
		channel.BasicPublish(
			exchange: "", 
			routingKey: "demo-queue", 
			basicProperties: null, 
			body: body);
	}
}