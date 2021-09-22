using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public interface IAsyncFileDeleter
    {
        Task DeleteAsync(string path);
    }
}
