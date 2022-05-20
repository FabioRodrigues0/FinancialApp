using CashBook.Api.Controllers;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CashBook.Api.Extentions;

public class RabbitQueueConsomer
{
	private readonly CashBookController _controller;
	public void ReceivedFromRabbit()
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

		var consumer = new EventingBasicConsumer(channel);
		consumer.Received += (sender, e) =>
		{
			var body = e.Body.ToArray();
			var message = Encoding.UTF8.GetString(body);
		};

		channel.BasicConsume("demo-Queue", true, consumer);
	}
}
