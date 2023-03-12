namespace BlazalR.Server.Extensions;
using Microsoft.AspNetCore.SignalR;
public static class SignalRServerExt
{
	public static Task SendMessageAsync<TMessage>(this IClientProxy proxy, TMessage arg, CancellationToken cancellationToken = default) => proxy.SendAsync(typeof(TMessage).Name, arg, cancellationToken);
}