﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Documents.Cache
{
    public sealed class MemoryDocumentCache<TMember>
        : IDocumentCache<TMember>
        , IDisposable
        where TMember : class
    {
        private readonly MemoryCacheEntryOptions cacheEntryOptions;
        private IMemoryCache cache;
        private bool disposedValue;

        public MemoryDocumentCache(MemoryCacheEntryOptions cacheEntryOptions)
        {
            this.cache = new MemoryCache(new MemoryCacheOptions());
            this.cacheEntryOptions = cacheEntryOptions ?? throw new ArgumentNullException(nameof(cacheEntryOptions));
            this.cacheEntryOptions.RegisterPostEvictionCallback(this.OnPostEviction);
        }

        public event EventHandler<CacheAccessedEventArgs> CacheAccessed;
        public event EventHandler<CacheItemEvictedEventArgs<TMember>> CacheItemEvicted;

        public void Clear()
        {
            var c = this.cache;
            try
            {
                this.cache = new MemoryCache(new MemoryCacheOptions());
            }
            finally
            {
                c.Dispose();
            }
        }

        public void Evict(string key)
        {
            this.cache.Remove(key);
        }

        public void Evict(Document<TMember> document)
        {
            this.Evict(document.Key);
        }

        public void InsertOrUpdate(Document<TMember> document)
        {
            this.cache.Set(document.Key, document, this.cacheEntryOptions);
        }

        public Task InsertOrUpdateAsync(IEnumerable<Document<TMember>> documents)
        {
            return Task.Run(() =>
            {
                foreach (var document in documents)
                {
                    this.InsertOrUpdate(document);
                }
            });
        }

        public Document<TMember> Read(string key, Func<string, Document<TMember>> itemFactory)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            if (itemFactory is null)
            {
                throw new ArgumentNullException(nameof(itemFactory));
            }

            var readType = CacheAccessType.Hit;

            var document = this.cache.GetOrCreate(key, entry =>
            {
                readType = CacheAccessType.Miss;
                entry.SetOptions(this.cacheEntryOptions);
                return itemFactory(key);
            });

            this.CacheAccessed?.Invoke(this, new CacheAccessedEventArgs(key, readType));

            return document;
        }

        public IEnumerable<Document<TMember>> Read(IEnumerable<string> keys, Func<string, Document<TMember>> itemFactory)
        {
            return keys is null
                ? throw new ArgumentNullException(nameof(keys))
                : keys.Select(key => this.Read(key, itemFactory));
        }

        public async Task<Document<TMember>> ReadAsync(string key, Func<string, Task<Document<TMember>>> asyncItemFactory)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            if (asyncItemFactory is null)
            {
                throw new ArgumentNullException(nameof(asyncItemFactory));
            }

            var readType = CacheAccessType.Hit;

            var document = await this.cache.GetOrCreateAsync(key, entry =>
            {
                readType = CacheAccessType.Miss;
                entry.SetOptions(this.cacheEntryOptions);
                return asyncItemFactory(key);
            });

            this.CacheAccessed?.Invoke(this, new CacheAccessedEventArgs(key, readType));

            return document;
        }

        public async Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys, Func<string, Task<Document<TMember>>> asyncItemFactory)
        {
            return keys is null
                ? throw new ArgumentNullException(nameof(keys))
                : await Task.WhenAll(keys.Select(key => this.ReadAsync(key, asyncItemFactory)));
        }

        private void OnPostEviction(object key, object value, EvictionReason reason, object state)
        {
            this.CacheItemEvicted?.Invoke(this, new CacheItemEvictedEventArgs<TMember>(value as Document<TMember>, reason));
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.cache.Dispose();
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}