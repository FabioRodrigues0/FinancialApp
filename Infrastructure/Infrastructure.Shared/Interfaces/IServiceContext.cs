﻿namespace Infrastructure.Shared.Interfaces;

public interface IServiceContext
{
	IReadOnlyCollection<string> Notifications { get; }

	bool HasNotification();

	bool HasContent();

	void NoContent(bool content);

	void AddNotification(string message);
}