namespace BlazalR.Shared.Model;

public static class MessageType
{
	public record OnPick(string Username, string ItemName, int Count);
	public record OnError(string Username, string Message);
}