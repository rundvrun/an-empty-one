namespace BlazalR.Server.Model
{
    public class InMemoryCounter : ICounter
    {
        public ConcurrentDictionary<string, int> Counter { get; } = new()
        {
            ["Item 1"] = 12,
            ["Item 2"] = 10
        };
    }
}