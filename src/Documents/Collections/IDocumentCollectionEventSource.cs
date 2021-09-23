using System;

namespace Documents.Collections
{
    public interface IDocumentCollectionEventSource
    {
        event EventHandler<EventArgs> Cleared;
    }
}
