using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    internal sealed class JsonDocumentEncoder<T>
        : IDocumentEncoder<T>
        where T : class
    {
        private readonly JsonSerializerSettings settings;

        public JsonDocumentEncoder(JsonSerializerSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task<string> SerializeAsync(Document<T> document)
        {
            return Task.Run(() => JsonConvert.SerializeObject(document, this.settings));
        }
    }
}
