using System;
using System.Collections.Generic;

namespace Documents.Collections
{
    public interface IDocumentCollection<TMember>
        : IEnumerable<Document<TMember>>
        where TMember : class
    {
        event EventHandler<DocumentAddedEventArgs<TMember>> DocumentAdded;
        event EventHandler<DocumentRemovedEventArgs<TMember>> DocumentRemoved;
        event EventHandler<DocumentUpdatedEventArgs<TMember>> DocumentUpdated;
        event EventHandler<EventArgs> Cleared;

        int Count { get; }

        IDocumentCollection<TMember> Add(Document<TMember> document);
        IDocumentCollection<TMember> Add(IEnumerable<Document<TMember>> documents);

        IDocumentCollection<TMember> Clear();

        bool Contains(string key);
        bool Contains(Document<TMember> document);

        Document<TMember> Read(string key);
        IEnumerable<Document<TMember>> Read(IEnumerable<string> keys);

        IDocumentCollection<TMember> Remove(string key);
        IDocumentCollection<TMember> Remove(IEnumerable<string> keys);
        IDocumentCollection<TMember> Remove(Document<TMember> document);
        IDocumentCollection<TMember> Remove(IEnumerable<Document<TMember>> documents);

        IDocumentCollection<TMember> Update(Document<TMember> document);
        IDocumentCollection<TMember> Update(IEnumerable<Document<TMember>> documents);
    }
}
