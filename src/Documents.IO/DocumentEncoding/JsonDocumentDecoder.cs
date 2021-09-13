using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Documents.IO.Encoding
{

    internal sealed class JsonDocumentDecoder<T>
        : IDocumentDecoder<T>
        where T : class
    {
        private readonly JsonSerializerSettings settings;

        public JsonDocumentDecoder(JsonSerializerSettings settings)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public Task<Document<T>> DeserializeAsync(string json)
        {
            return Task.Run(() => JsonConvert.DeserializeObject<Document<T>>(json, this.settings));
        }
    }
}
