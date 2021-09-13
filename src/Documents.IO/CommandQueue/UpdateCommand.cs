namespace Documents.IO.CommandQueue
{
    public abstract class UpdateCommand<TMember>
        : DocumentCommand<TMember>
        where TMember : class
    {
        protected UpdateCommand(Document<TMember> document)
            : base(document)
        {
        }
    }
}
