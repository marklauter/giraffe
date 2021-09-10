using System;
using System.Collections.Generic;

namespace Documents
{
    public interface IDocumentCollection<TMember>
        : IEnumerable<Document<TMember>>
        where TMember : class
    {
        event EventHandler<DocumentAddedEventArgs<TMember>> AfterAdd;
        event EventHandler<DocumentRemovedEventArgs<TMember>> AfterRemove;
        event EventHandler<DocumentUpdatedEventArgs<TMember>> AfterUpdate;
        event EventHandler<EventArgs> Cleared;

        int Count { get; }

        void Add(Document<TMember> document);
        void Add(IEnumerable<Document<TMember>> documents);

        void Clear();

        bool Contains(string key);
        bool Contains(Document<TMember> document);

        Document<TMember> Read(string key);
        IEnumerable<Document<TMember>> Read(IEnumerable<string> keys);

        void Remove(string key);
        void Remove(IEnumerable<string> keys);
        void Remove(Document<TMember> document);
        void Remove(IEnumerable<Document<TMember>> documents);

        void Update(Document<TMember> document);
        void Update(IEnumerable<Document<TMember>> documents);
    }
}
