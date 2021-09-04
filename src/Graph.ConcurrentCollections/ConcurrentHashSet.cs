using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace Graph.ConcurrentCollections
{
    // todo: look at how ConcurrentDictionary uses Monitor and AquireAllLocks to lock elements in the array. That's better / faster / more targeted locking than using lock-slim.
    // todo: see: https://referencesource.microsoft.com/#mscorlib/system/Collections/Concurrent/ConcurrentDictionary.cs,5175a7680b0f7188

    [JsonObject("hashSet")]
    public sealed class ConcurrentHashSet<T>
        : IEnumerable<T>
        , ICloneable
    {
        [JsonProperty("items")]
        private readonly HashSet<T> items = new();

        private readonly ReaderWriterLockSlim gate = new();

        [Pure]
        [JsonIgnore]
        public int Count
        {
            get
            {
                this.gate.EnterReadLock();
                try
                {
                    return this.items.Count;
                }
                finally
                {
                    this.gate.ExitReadLock();
                }
            }
        }

        public ConcurrentHashSet() { }

        private ConcurrentHashSet(ConcurrentHashSet<T> other)
        {
            this.items.UnionWith(other.ToArray());
        }

        public bool Add(T item)
        {
            this.gate.EnterWriteLock();
            try
            {
                return this.items.Add(item);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public void Clear()
        {
            this.gate.EnterWriteLock();
            try
            {
                this.items.Clear();
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        [Pure]
        public object Clone()
        {
            return new ConcurrentHashSet<T>(this);
        }

        [Pure]
        public bool Contains(T item)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.items.Contains(item);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public int EnsureCapacity(int capacity)
        {
            this.gate.EnterWriteLock();
            try
            {
                return this.items.EnsureCapacity(capacity);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                this.items.ExceptWith(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                this.items.IntersectWith(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.items.IsSubsetOf(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.items.IsSupersetOf(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public bool Overlaps(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.items.Overlaps(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public bool Remove(T item)
        {
            this.gate.EnterWriteLock();
            try
            {
                return this.items.Remove(item);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        [Pure]
        public bool SetEquals(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.items.SetEquals(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        public T[] ToArray()
        {
            var array = new T[this.Count];
            this.gate.EnterReadLock();
            try
            {
                var i = 0;
                foreach (var item in this.items)
                {
                    array[i] = item;
                    ++i;
                }

                return array;
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public void UnionWith(IEnumerable<T> other)
        {
            this.gate.EnterWriteLock();
            try
            {
                this.items.UnionWith(other);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        [Pure]
        public IEnumerator<T> GetEnumerator()
        {
            this.gate.EnterReadLock();
            try
            {
                foreach (var item in this.items)
                {
                    yield return item;
                }
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
