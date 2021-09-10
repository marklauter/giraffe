using System;

namespace Documents
{
    public abstract class DocumentEventArgs<T>
        : EventArgs
        where T : class
    {
        protected DocumentEventArgs(Document<T> document)
        {
            this.Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public Document<T> Document { get; }
    }
}
