namespace Documents.IO.CommandQueue
{
    public abstract class RemoveCommand<TMember>
        : DocumentCommand<TMember>
         where TMember : class
    {
        protected RemoveCommand(Document<TMember> document)
            : base(document)
        {
        }
    }
}
