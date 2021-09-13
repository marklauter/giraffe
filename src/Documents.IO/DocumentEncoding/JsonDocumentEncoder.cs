using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Documents.IO.Encoding
{
    internal sealed class JsonDocumentEncoder<TMember>
        : IDocumentEncoder<TMember>
        where TMember : class
    {
        private readonly JsonSerializerSettings settings;

        public JsonDocumentEncoder(JsonSerializerSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task<string> SerializeAsync(Document<TMember> document)
        {
            return Task.Run(() => JsonConvert.SerializeObject(document, this.settings));
        }
    }
}
