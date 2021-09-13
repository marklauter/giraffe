using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    public interface IDocumentEncoder<T>
        where T : class
    {
        Task<string> SerializeAsync(Document<T> document);
    }
}
