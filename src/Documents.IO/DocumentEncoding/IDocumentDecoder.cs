using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    public interface IDocumentDecoder<T>
        where T : class
    {
        Task<Document<T>> DeserializeAsync(string json);
    }
}
