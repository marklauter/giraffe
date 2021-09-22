using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public interface IAsyncFileWriter
    {
        Task WriteAsync(string path, string text);
    }
}
