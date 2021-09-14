using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Cache
{
    public interface IDocumentCache<TMember>
        : IDocumentCacheEventSource<TMember>
        where TMember : class
    {
        void Clear();

        void Evict(string key);
        void Evict(Document<TMember> document);

        void InsertOrUpdate(Document<TMember> document);
        Task InsertOrUpdateAsync(IEnumerable<Document<TMember>> documents);

        Document<TMember> Read(string key, Func<string, Document<TMember>> itemFactory);
        IEnumerable<Document<TMember>> Read(IEnumerable<string> keys, Func<string, Document<TMember>> itemFactory);

        Task<Document<TMember>> ReadAsync(string key, Func<string, Task<Document<TMember>>> asyncItemFactory);
        Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys, Func<string, Task<Document<TMember>>> asyncItemFactory);
    }
}
