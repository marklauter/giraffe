﻿using Microsoft.Extensions.Caching.Memory;

namespace Documents.Cache
{
    public class CacheItemEvictedEventArgs<T>
        : DocumentEventArgs<T>
        where T : class
    {
        public CacheItemEvictedEventArgs(Document<T> document, EvictionReason reason)
            : base(document)
        {
            this.Reason = reason;
        }

        public EvictionReason Reason { get; }
    }
}
