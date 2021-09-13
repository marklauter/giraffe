namespace Documents.IO.CommandQueue
{
    public abstract class AddCommand<TMember>
        : DocumentCommand<TMember>
         where TMember : class
    {
        protected AddCommand(Document<TMember> document)
            : base(document)
        {
        }
    }
}
