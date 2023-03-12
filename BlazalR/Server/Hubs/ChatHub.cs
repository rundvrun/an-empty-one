using Microsoft.AspNetCore.SignalR;
namespace BlazalR.Server.Hubs;

public class ChatHub : Hub
{
	const string onPick = nameof(MessageType.OnPick);
	const string onError = nameof(MessageType.OnError);
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
				await Clients.All.SendAsync(onError, new MessageType.OnError(user, $"{strItem} is run out"));
				return;
			}

			count = int.Min(_counter.Counter[strItem], count);
			_counter.Counter[strItem] -= count;

			await Clients.All.SendAsync(onPick, new MessageType.OnPick(user, strItem, count));
			return;
		}
		await Clients.All.SendAsync(onError, new MessageType.OnError(user, "Invalid item!"));
	}
}