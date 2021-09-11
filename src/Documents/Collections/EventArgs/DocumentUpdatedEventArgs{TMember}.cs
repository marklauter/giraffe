namespace Documents.Collections
{
    public class DocumentUpdatedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public DocumentUpdatedEventArgs(Document<TMember> document)
            : base(document)
        {
        }
    }
}
