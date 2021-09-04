using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Graph.ConcurrentCollections
{
    public sealed class ConcurrentHashSet<T>
        : IEnumerable<T>
    {
        private readonly HashSet<T> hashset = new();
        private readonly ReaderWriterLockSlim gate = new();

        public ConcurrentHashSet() { }

        public ConcurrentHashSet(ConcurrentHashSet<T> other)
        {
            this.hashset.UnionWith(other.hashset);
        }

        public bool Add(T item)
        {
            this.gate.EnterWriteLock();
            try
            {
                return this.hashset.Add(item);
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
                this.hashset.Clear();
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public bool Contains(T item)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.hashset.Contains(item);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public int Count
        {
            get
            {
                this.gate.EnterReadLock();
                try
                {
                    return this.hashset.Count;
                }
                finally
                {
                    this.gate.ExitReadLock();
                }
            }
        }

        public int EnsureCapacity(int capacity)
        {
            this.gate.EnterWriteLock();
            try
            {
                return this.hashset.EnsureCapacity(capacity);
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
                this.hashset.ExceptWith(other);
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
                this.hashset.IntersectWith(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.hashset.IsSubsetOf(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.hashset.IsSupersetOf(other);
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.hashset.Overlaps(other);
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
                return this.hashset.Remove(item);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            this.gate.EnterReadLock();
            try
            {
                return this.hashset.SetEquals(other);
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
                this.hashset.UnionWith(other);
            }
            finally
            {
                this.gate.ExitWriteLock();
            }
        }

        public bool IsReadOnly => ((ICollection<T>)this.hashset).IsReadOnly;

        public IEnumerator<T> GetEnumerator()
        {
            this.gate.EnterReadLock();
            try
            {
                foreach (var item in this.hashset)
                {
                    yield return item;
                }
            }
            finally
            {
                this.gate.ExitReadLock();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
