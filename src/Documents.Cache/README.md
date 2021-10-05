# Documents.Cache Namespace
The Documents.Cache implements a memory cache and provides interfaces to allow the creation of wrappers for other external cache systems, such as Redis.

## Exports
 - IDocumentCacheEventSource
 - IDocumentCahce : IDocumentCacheEventSource
 - MemoryDocumentCache : IDocumentCahce
 - Event Args
 -- CacheAccessedEventArgs
 -- CacheItemEvictedEventArgs