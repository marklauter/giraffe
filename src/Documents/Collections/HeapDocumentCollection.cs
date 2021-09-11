using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;

namespace Documents.Collections
{
    public sealed class HeapDocumentCollection<TMember>
        : DocumentCollection<TMember>
        where TMember : class
    {
        private ImmutableDictionary<string, Document<TMember>> documents = ImmutableDictionary<string, Document<TMember>>.Empty;

        public static HeapDocumentCollection<TMember> Empty => new();

        public override int Count => this.documents.Count;

        public override IEnumerator<Document<TMember>> GetEnumerator()
        {
            foreach (var document in this.documents.Values)
            {
                yield return document;
            }
        }

        protected override void AddDocument([Pure] Document<TMember> document)
        {
            this.documents = this.documents.ContainsKey(document.Key)
                ? throw new InvalidOperationException($"Document with key '{document.Key}' is already in the collection.")
                : this.documents.Add(document.Key, document);
        }

        protected override void ClearCollection()
        {
            this.documents = ImmutableDictionary<string, Document<TMember>>.Empty;
        }

        protected override bool ContainsDocument(string key)
        {
            return this.documents.ContainsKey(key);
        }

        protected override Document<TMember> ReadDocument(string key)
        {
            return this.documents.TryGetValue(key, out var document)
                ? document
                : throw new KeyNotFoundException(key);
        }

        protected override void RemoveDocument(string key)
        {
            this.documents = this.documents.ContainsKey(key)
                ? this.documents.Remove(key)
                : throw new KeyNotFoundException(key);
        }

        protected override void UpdateDocument([Pure] Document<TMember> document)
        {
            this.documents = this.documents.ContainsKey(document.Key)
                ? this.documents.SetItem(document.Key, document)
                : throw new KeyNotFoundException(document.Key);
        }
    }
}
