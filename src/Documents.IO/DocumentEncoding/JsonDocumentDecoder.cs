using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Documents.IO.Encoding
{

    internal sealed class JsonDocumentDecoder<TMember>
        : IDocumentDecoder<TMember>
        where TMember : class
    {
        private readonly JsonSerializerSettings settings;

        public JsonDocumentDecoder(JsonSerializerSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task<Document<TMember>> DeserializeAsync(string json)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<Document<TMember>>(json, this.settings));
        }
    }
}
