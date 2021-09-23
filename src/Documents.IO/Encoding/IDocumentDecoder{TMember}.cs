using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    public interface IDocumentDecoder<TMember>
        where TMember : class
    {
        Task<Document<TMember>> DeserializeAsync(string json);
    }
}
