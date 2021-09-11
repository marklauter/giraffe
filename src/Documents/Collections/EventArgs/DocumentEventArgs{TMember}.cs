using System;

namespace Documents.Collections
{
    public abstract class DocumentEventArgs<TMember>
        : EventArgs
        where TMember : class
    {
        protected DocumentEventArgs(Document<TMember> document)
        {
            this.Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public Document<TMember> Document { get; }
    }
}
