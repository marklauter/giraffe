namespace Documents.Collections
{
    public sealed class DocumentRemovedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public DocumentRemovedEventArgs(Document<TMember> document)
            : base(document)
        {
        }
    }
}
