using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace MessageBroker.UnitTest;

public class MessageBrokerTest : RabbitInstances
{
	[Fact]
	public async Task RabbitMq_Publisher_Test()
	{
		#region Arrange

		var cashbook = new CashBookFaker().cashbookDto;
		//Mock Instances
		var publisher = CreatePublisherInstance();

		#endregion Arrange

		#region Act

		var taskResult = Task.Run(() => { publisher.SendToRabbit(cashbook, "QueueNameCB"); });

		#endregion Act

		#region Assert

		await taskResult.ShouldNotThrowAsync();

		#endregion Assert
	}

	[Fact]
	public async Task RabbitMq_Consumer_Test()
	{
		#region Arrange

		var cashbook = new CashBookFaker().cashbookDto;
		//Mock Instances
		var consumer = CreateConsumerInstance();

		#endregion Arrange

		#region Act

		var taskResult = Task.Run(() => { consumer.ReceivedFromRabbit(); });

		#endregion Act

		#region Assert

		await taskResult.ShouldNotThrowAsync();

		#endregion Assert
	}
}
