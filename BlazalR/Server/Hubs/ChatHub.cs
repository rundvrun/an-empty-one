namespace BlazalR.Server.Hubs;

using Microsoft.AspNetCore.SignalR;
using BlazalR.Server.Extensions;

public class ChatHub : Hub
{
	readonly ICounter _counter;
	readonly ILogger<ChatHub> _logger;
	public ChatHub(ICounter counter, ILogger<ChatHub> logger) : base()
	{
		_counter = counter;
		_logger = logger;
	}

	public async Task PickItem(string user, string strItem)
	{
		var rng = new Random();
		var count = rng.Next(3);

		try
		{
			if (_counter.Counter.ContainsKey(strItem))
			{
				if (_counter.Counter[strItem] == 0)
				{
					await Clients.All.SendMessageAsync<MessageType.OnError>(new(user, $"{strItem} is run out"));
					return;
				}

				count = int.Min(_counter.Counter[strItem], count);
				_counter.Counter[strItem] -= count;

				await Clients.All.SendMessageAsync<MessageType.OnPick>(new(user, strItem, count));
				return;
			}
			await Clients.All.SendMessageAsync<MessageType.OnError>(new(user, "Invalid item!"));
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Exception picking item", "ChatHub/PickItem", user, strItem);
		}
	}
}