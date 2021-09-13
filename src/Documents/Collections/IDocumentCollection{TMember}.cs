using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Collections
{
    public interface IDocumentCollection<TMember>
        where TMember : class
    {
        event EventHandler<DocumentAddedEventArgs<TMember>> DocumentAdded;
        event EventHandler<DocumentRemovedEventArgs<TMember>> DocumentRemoved;
        event EventHandler<DocumentUpdatedEventArgs<TMember>> DocumentUpdated;
        event EventHandler<EventArgs> Cleared;

        int Count { get; }

        Task AddAsync(Document<TMember> document);
        IEnumerable<Task> AddAsync(IEnumerable<Document<TMember>> documents);

        Task ClearAsync();

        Task<bool> ContainsAsync(string key);
        Task<bool> ContainsAsync(Document<TMember> document);

        Task<Document<TMember>> ReadAsync(string key);
        Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys);

        Task RemoveAsync(string key);
        Task RemoveAsync(IEnumerable<string> keys);
        Task RemoveAsync(Document<TMember> document);
        Task RemoveAsync(IEnumerable<Document<TMember>> documents);

        Task UpdateAsync(Document<TMember> document);
        Task UpdateAsync(IEnumerable<Document<TMember>> documents);
    }
}
