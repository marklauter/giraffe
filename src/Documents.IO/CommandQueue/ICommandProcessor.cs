using System.Threading.Tasks;

namespace Documents.IO.CommandQueue
{
    public interface ICommandProcessor<TCommand, TMember>
        where TCommand : DocumentCommand<TMember>
        where TMember : class
    {
        Task ExecuteAsyc(TCommand command);
    }
}
