namespace BlazalR.Server.Hubs;

using Microsoft.AspNetCore.SignalR;
using BlazalR.Server.Extensions;

public class ChatHub : Hub
{
	readonly ICounter _counter;
	public ChatHub(ICounter counter) : base()
	{
		_counter = counter;
	}

	public async Task PickItem(string user, string strItem)
	{
		var rng = new Random();
		var count = rng.Next(3);

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
}