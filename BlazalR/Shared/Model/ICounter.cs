using System.Collections.Concurrent;
namespace BlazalR.Shared.Model;

public interface ICounter
{
    public ConcurrentDictionary<string, int> Counter { get; }
}