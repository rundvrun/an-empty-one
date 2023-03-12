namespace BlazalR.Shared.Model;

public static class MessageType
{
    public record OnPick
    {
        public string? Username { get; init; }
        public string? ItemName { get; init; }
        public int Count { get; set; }
    }

    public record OnError
    {
        public string? Username { get; set; }
        public string? Message { get; set; }
    }
}