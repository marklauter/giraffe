namespace Documents.Collections
{
    public class DocumentAddedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public DocumentAddedEventArgs(Document<TMember> document)
            : base(document)
        {
        }
    }
}
