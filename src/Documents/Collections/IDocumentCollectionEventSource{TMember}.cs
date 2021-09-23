using System;

namespace Documents.Collections
{
    public interface IDocumentCollectionEventSource<TMember>
        : IDocumentCollectionEventSource
        where TMember : class
    {
        event EventHandler<DocumentAddedEventArgs<TMember>> DocumentAdded;
        event EventHandler<DocumentRemovedEventArgs<TMember>> DocumentRemoved;
        event EventHandler<DocumentUpdatedEventArgs<TMember>> DocumentUpdated;
    }
}
