using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;

namespace Documents.Collections
{
    public sealed class HeapDocumentCollection<TMember>
        : DocumentCollection<TMember>
        where TMember : class
    {
        private readonly ConcurrentDictionary<string, Document<TMember>> documents = new();

        public static HeapDocumentCollection<TMember> Empty => new();

        public override int Count => this.documents.Count;

        protected override Task ClearCollectionAsync()
        {
            this.documents.Clear();

            return Task.CompletedTask;
        }

        protected override Task<bool> ContainsDocumentAsync(string key)
        {
            return Task.FromResult(this.documents.ContainsKey(key));
        }

        protected override Task<Document<TMember>> ReadDocumentAsync(string key)
        {
            return this.documents.TryGetValue(key, out var document)
                ? Task.FromResult(document)
                : throw new KeyNotFoundException(key);
        }

        protected override Task RemoveDocumentAsync(string key)
        {
            return this.documents.TryRemove(key, out var _)
                ? Task.CompletedTask
                : throw new KeyNotFoundException(key);
        }

        protected override Task WriteDocumentAsync([Pure] Document<TMember> document)
        {
            this.documents[document.Key] = document;
            return Task.CompletedTask;
        }
    }
}
