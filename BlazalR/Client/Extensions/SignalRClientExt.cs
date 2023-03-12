using Microsoft.AspNetCore.SignalR.Client;

namespace BlazalR.Client.Extensions;

public static class SignalRClientExt
{
	public static IDisposable OnMessage<TMessage>(this HubConnection hubConn, Action<TMessage> handler) => hubConn.On<TMessage>(typeof(TMessage).Name, handler);
}