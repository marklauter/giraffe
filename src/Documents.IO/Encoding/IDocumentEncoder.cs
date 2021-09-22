using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    public interface IDocumentEncoder<TMember>
        where TMember : class
    {
        Task<string> SerializeAsync(Document<TMember> document);
    }
}
