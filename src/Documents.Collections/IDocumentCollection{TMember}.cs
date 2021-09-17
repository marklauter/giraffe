using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Collections
{
    public interface IDocumentCollection<TMember>
        : IDocumentCollection
        , IDocumentCollectionEventSource<TMember>
        where TMember : class
    {
        Task AddAsync(Document<TMember> document);
        Task AddAsync(IEnumerable<Document<TMember>> documents);

        Task<bool> ContainsAsync(Document<TMember> document);

        Task<Document<TMember>> ReadAsync(string key);
        Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys);

        Task RemoveAsync(Document<TMember> document);
        Task RemoveAsync(IEnumerable<Document<TMember>> documents);

        Task UpdateAsync(Document<TMember> document);
        Task UpdateAsync(IEnumerable<Document<TMember>> documents);
    }
}
