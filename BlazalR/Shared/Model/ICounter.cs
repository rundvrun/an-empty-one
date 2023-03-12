using System.Collections.Concurrent;
namespace BlazalR.Shared.Model;

public interface ICounter
{
    ConcurrentDictionary<string, int> Counter { get; }
}