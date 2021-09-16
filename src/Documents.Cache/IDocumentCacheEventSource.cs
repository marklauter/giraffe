using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents.Cache
{
    public interface IDocumentCacheEventSource<TMember>
        where TMember : class
    {
        event EventHandler<CacheAccessedEventArgs> CacheAccessed;
        event EventHandler<CacheItemEvictedEventArgs<TMember>> CacheItemEvicted;
    }
}
