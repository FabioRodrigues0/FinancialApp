using Infrastructure.Shared.Services.Interface;

namespace Infrastructure.Shared.Services
{
	public class ServiceContext : IServiceContext
	{
		private readonly List<string> _notifications;
		private bool _content;

		public ServiceContext()
		{
			_notifications = new List<string>();
			_content = true;
		}

		public IReadOnlyCollection<string> Notifications {
			get { return _notifications.AsReadOnly(); }
		}

		public bool HasNotification() => Notifications.Count > 0;

		public bool HasContent() => _content;

		public void NoContent(bool content)
		{
			_content = content;
		}

		public void AddNotification(string message)
		{
			if (!Notifications.Contains(message))
				_notifications.Add(message);
		}
	}
}
