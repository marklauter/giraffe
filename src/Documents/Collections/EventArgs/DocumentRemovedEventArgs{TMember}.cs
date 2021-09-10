namespace Documents
{
    public class DocumentRemovedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public DocumentRemovedEventArgs(Document<TMember> document)
            : base(document)
        {
        }
    }
}
