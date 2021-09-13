using System;

namespace Documents.IO.CommandQueue
{
    public abstract class DocumentCommand<TMember>
        where TMember : class
    {
        protected DocumentCommand(Document<TMember> document)
        {
            this.Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        public Document<TMember> Document { get; }
    }
}
