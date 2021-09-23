using Microsoft.Extensions.Caching.Memory;

namespace Documents.Cache
{
    public class CacheItemEvictedEventArgs<TMember>
        : DocumentEventArgs<TMember>
        where TMember : class
    {
        public CacheItemEvictedEventArgs(Document<TMember> document, EvictionReason reason)
            : base(document)
        {
            this.Reason = reason;
        }

        public EvictionReason Reason { get; }
    }
}
