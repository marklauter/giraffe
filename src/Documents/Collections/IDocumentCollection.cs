using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Collections
{
    public interface IDocumentCollection
        : IDocumentCollectionEventSource
    {
        int Count { get; }
        bool IsEmpty { get; }
        Task ClearAsync();
        Task<bool> ContainsAsync(string key);
        Task RemoveAsync(string key);
        Task RemoveAsync(IEnumerable<string> keys);
    }
}
