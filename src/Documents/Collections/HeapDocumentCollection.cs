using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.Collections
{
    public sealed class HeapDocumentCollection<TMember>
        : DocumentCollection<TMember>
        where TMember : class
    {
        private ImmutableDictionary<string, Document<TMember>> documents = ImmutableDictionary<string, Document<TMember>>.Empty;

        public static HeapDocumentCollection<TMember> Empty => new();

        public override int Count => this.documents.Count;

        protected override Task ClearCollectionAsync()
        {
            this.documents = ImmutableDictionary<string, Document<TMember>>.Empty;

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
            this.documents = this.documents.ContainsKey(key)
                ? this.documents.Remove(key)
                : throw new KeyNotFoundException(key);

            return Task.CompletedTask;
        }

        protected override Task WriteDocumentAsync([Pure] Document<TMember> document)
        {
            this.documents = this.documents.SetItem(document.Key, document);

            return Task.CompletedTask;
        }
    }
}
