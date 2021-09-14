using Documents.Tests;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using Xunit;

namespace Documents.Cache.Tests
{
    public class DocumentCacheTests
    {
        private Document<Value> DocumentFactory(string key)
        {
            return Document.FromMember(new Value(Int32.Parse(key)) { Text = "one" });
        }

        [Fact]
        public void CacheExperiment_Proves_Single_Instance_of_Document()
        {
            var key = "1";
            var cache = new MemoryDocumentCache<Value>(new MemoryCacheEntryOptions());
            var document = cache.Read(key, this.DocumentFactory);
            var value = (Value)document;
            Assert.Equal("one", value.Text);

            value.Text = "two";

            document = cache.Read(key, this.DocumentFactory);
            var copy = (Value)document;

            Assert.Equal(value.Text, copy.Text);
        }

        [Fact]
        public void Cache_EventArgs()
        {
            var document = (Document<Member>)new Member();

            var documentEventArgs = new CacheItemEvictedEventArgs<Member>(document, EvictionReason.Removed);
            Assert.Equal(document, documentEventArgs.Document);
            Assert.Equal(EvictionReason.Removed, documentEventArgs.Reason);

            var readArgs = new CacheAccessedEventArgs(document.Key, CacheAccessType.Hit);
            Assert.Equal(document.Key, readArgs.Key);
            Assert.Equal(CacheAccessType.Hit, readArgs.ReadType);

            Assert.Throws<ArgumentNullException>(() => _ = new CacheItemEvictedEventArgs<Member>(null, EvictionReason.None));
            Assert.Throws<ArgumentException>(() => _ = new CacheAccessedEventArgs(null, CacheAccessType.Hit));
            Assert.Throws<ArgumentException>(() => _ = new CacheAccessedEventArgs(" ", CacheAccessType.Hit));
        }

        [Fact]
        public void Cache_Read_Factory_Succeeds_And_Hit_Miss_Event_Raised()
        {
            var key = "1";
            var hash = new HashSet<Document<Value>>();
            using var cache = new MemoryDocumentCache<Value>(new MemoryCacheEntryOptions { SlidingExpiration = System.TimeSpan.FromSeconds(60) });

            var e = Assert.Raises<CacheAccessedEventArgs>(handler => cache.CacheAccessed += handler, handler => cache.CacheAccessed -= handler, () =>
            {
                hash.Add(cache.Read(key, this.DocumentFactory));
            });

            Assert.Equal(cache, e.Sender);
            Assert.Equal(key, e.Arguments.Key);
            Assert.Equal(CacheAccessType.Miss, e.Arguments.ReadType);

            e = Assert.Raises<CacheAccessedEventArgs>(handler => cache.CacheAccessed += handler, handler => cache.CacheAccessed -= handler, () =>
            {
                hash.Add(cache.Read(key, this.DocumentFactory));
            });

            Assert.Equal(cache, e.Sender);
            Assert.Equal(key, e.Arguments.Key);
            Assert.Equal(CacheAccessType.Hit, e.Arguments.ReadType);

            Assert.Single(hash);
        }

        [Fact]
        public void Cache_Evict_Succeeds()
        {
            var key = "1";
            using var cache = new MemoryDocumentCache<Value>(new MemoryCacheEntryOptions { SlidingExpiration = System.TimeSpan.FromSeconds(60) });
            Document<Value> v = null;

            var cacheAccessedEvent = Assert.Raises<CacheAccessedEventArgs>(
                handler => cache.CacheAccessed += handler,
                handler => cache.CacheAccessed -= handler,
                () =>
                {
                    v = cache.Read(key, this.DocumentFactory);
                });

            Assert.Equal(cache, cacheAccessedEvent.Sender);
            Assert.Equal(key, cacheAccessedEvent.Arguments.Key);
            Assert.Equal(CacheAccessType.Miss, cacheAccessedEvent.Arguments.ReadType);

            cache.Evict(v);

            cacheAccessedEvent = Assert.Raises<CacheAccessedEventArgs>(
                handler => cache.CacheAccessed += handler,
                handler => cache.CacheAccessed -= handler,
                () =>
                {
                    v = cache.Read(key, this.DocumentFactory);
                });

            Assert.Equal(cache, cacheAccessedEvent.Sender);
            Assert.Equal(key, cacheAccessedEvent.Arguments.Key);
            Assert.Equal(CacheAccessType.Miss, cacheAccessedEvent.Arguments.ReadType);
        }

        [Fact]
        public void Cache_Clear_Succeeds()
        {
            var key = "1";
            using var cache = new MemoryDocumentCache<Value>(new MemoryCacheEntryOptions { SlidingExpiration = System.TimeSpan.FromSeconds(60) });
            Document<Value> v = null;

            var cacheAccessedEvent = Assert.Raises<CacheAccessedEventArgs>(
                handler => cache.CacheAccessed += handler,
                handler => cache.CacheAccessed -= handler,
                () =>
                {
                    v = cache.Read(key, this.DocumentFactory);
                });

            Assert.Equal(cache, cacheAccessedEvent.Sender);
            Assert.Equal(key, cacheAccessedEvent.Arguments.Key);
            Assert.Equal(CacheAccessType.Miss, cacheAccessedEvent.Arguments.ReadType);

            cache.Clear();

            cacheAccessedEvent = Assert.Raises<CacheAccessedEventArgs>(
                handler => cache.CacheAccessed += handler,
                handler => cache.CacheAccessed -= handler,
                () =>
                {
                    v = cache.Read(key, this.DocumentFactory);
                });

            Assert.Equal(cache, cacheAccessedEvent.Sender);
            Assert.Equal(key, cacheAccessedEvent.Arguments.Key);
            Assert.Equal(CacheAccessType.Miss, cacheAccessedEvent.Arguments.ReadType);
        }
    }

}
