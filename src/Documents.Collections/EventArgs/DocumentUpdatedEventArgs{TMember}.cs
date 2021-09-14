namespace Documents.Collections
{
    public sealed class DocumentUpdatedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public DocumentUpdatedEventArgs(Document<TMember> document)
            : base(document)
        {
        }
    }
}
