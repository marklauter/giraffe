using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public interface IAsyncFileReader
    {
        Task<string> ReadAsync(string path);
    }
}
