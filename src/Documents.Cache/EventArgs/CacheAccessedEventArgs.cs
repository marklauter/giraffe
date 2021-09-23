using System;

namespace Documents.Cache
{
    public sealed class CacheAccessedEventArgs
        : EventArgs
    {
        public CacheAccessedEventArgs(string key, CacheAccessType accessType)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            this.Key = key;
            this.AccessType = accessType;
        }

        public string Key { get; }

        public CacheAccessType AccessType { get; }
    }
}
