using Documents.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Cache
{
    public sealed class CachedDocumentCollection<TMember>
        : IDocumentCollection<TMember>
        , IDocumentCacheEventSource<TMember>
        where TMember : class
    {
        private readonly IDocumentCollection<TMember> documentCollection;
        private readonly IDocumentCache<TMember> documentCache;

        public CachedDocumentCollection(
            IDocumentCollection<TMember> documentCollection,
            IDocumentCache<TMember> documentCache)
        {
            this.documentCollection = documentCollection ?? throw new ArgumentNullException(nameof(documentCollection));
            this.documentCache = documentCache ?? throw new ArgumentNullException(nameof(documentCache));

            documentCollection.Cleared += this.DocumentCollection_Cleared;
            documentCollection.DocumentAdded += this.DocumentCollection_DocumentAdded;
            documentCollection.DocumentRemoved += this.DocumentCollection_DocumentRemoved;
            documentCollection.DocumentUpdated += this.DocumentCollection_DocumentUpdated;

            this.documentCache.CacheAccessed += this.DocumentCache_CacheAccessed;
            this.documentCache.CacheItemEvicted += this.DocumentCache_CacheItemEvicted;
        }

        public int Count => this.documentCollection.Count;

        public bool IsEmpty => this.documentCollection.IsEmpty;

        public event EventHandler<DocumentAddedEventArgs<TMember>> DocumentAdded;

        public event EventHandler<DocumentRemovedEventArgs<TMember>> DocumentRemoved;

        public event EventHandler<DocumentUpdatedEventArgs<TMember>> DocumentUpdated;

        public event EventHandler<EventArgs> Cleared;

        public event EventHandler<CacheAccessedEventArgs> CacheAccessed;

        public event EventHandler<CacheItemEvictedEventArgs<TMember>> CacheItemEvicted;

        public Task AddAsync(Document<TMember> document)
        {
            return this.documentCollection.AddAsync(document);
        }

        public Task AddAsync(IEnumerable<Document<TMember>> documents)
        {
            return this.documentCollection.AddAsync(documents);
        }

        public Task ClearAsync()
        {
            return this.documentCollection.ClearAsync();
        }

        public Task<bool> ContainsAsync(string key)
        {
            return this.documentCollection.ContainsAsync(key);
        }

        public Task<bool> ContainsAsync(Document<TMember> document)
        {
            return this.documentCollection.ContainsAsync(document);
        }

        public Task<Document<TMember>> ReadAsync(string key)
        {
            return this.documentCache.ReadAsync(key, this.documentCollection.ReadAsync);
        }

        public Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys)
        {
            return this.documentCache.ReadAsync(keys, this.documentCollection.ReadAsync);
        }

        public Task RemoveAsync(string key)
        {
            return this.documentCollection.RemoveAsync(key);
        }

        public Task RemoveAsync(IEnumerable<string> keys)
        {
            return this.documentCollection.RemoveAsync(keys);
        }

        public Task RemoveAsync(Document<TMember> document)
        {
            return this.documentCollection.RemoveAsync(document);
        }

        public Task RemoveAsync(IEnumerable<Document<TMember>> documents)
        {
            return this.documentCollection.RemoveAsync(documents);
        }

        public Task UpdateAsync(Document<TMember> document)
        {
            return this.documentCollection.UpdateAsync(document);
        }

        public Task UpdateAsync(IEnumerable<Document<TMember>> documents)
        {
            return this.documentCollection.UpdateAsync(documents);
        }

        private void DocumentCollection_DocumentAdded(object sender, DocumentAddedEventArgs<TMember> e)
        {
            this.documentCache.InsertOrUpdate(e.Document);
            this.DocumentAdded?.Invoke(this, e);
        }

        private void DocumentCollection_DocumentRemoved(object sender, DocumentRemovedEventArgs<TMember> e)
        {
            this.documentCache.Evict(e.Document);
            this.DocumentRemoved?.Invoke(this, e);
        }

        private void DocumentCollection_DocumentUpdated(object sender, DocumentUpdatedEventArgs<TMember> e)
        {
            this.documentCache.InsertOrUpdate(e.Document);
            this.DocumentUpdated?.Invoke(this, e);
        }

        private void DocumentCollection_Cleared(object sender, EventArgs e)
        {
            this.documentCache.Clear();
            this.Cleared?.Invoke(this, e);
        }

        private void DocumentCache_CacheItemEvicted(object sender, CacheItemEvictedEventArgs<TMember> e)
        {
            this.CacheItemEvicted?.Invoke(this, e);
        }

        private void DocumentCache_CacheAccessed(object sender, CacheAccessedEventArgs e)
        {
            this.CacheAccessed?.Invoke(this, e);
        }
    }
}
