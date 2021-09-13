using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Collections.Concurrent
{
    public sealed class ConcurrentHashSet<T>
        : ISet<T>
        , ICloneable
    {
        private readonly HashSet<T> hashset;
        private readonly Gate gate = new();

        public static ConcurrentHashSet<T> Empty => new();

        private ConcurrentHashSet()
        {
            this.hashset = new();
        }

        private ConcurrentHashSet(ConcurrentHashSet<T> other)
        {
            this.hashset = new(other);
        }

        public ConcurrentHashSet(IEnumerable<T> items)
        {
            this.hashset = new(items);
        }

        public int Count => this.gate.Read(() => this.hashset.Count);

        public bool IsEmpty => this.Count == 0;

        public bool IsReadOnly => false;

        public bool Add(T item)
        {
            return this.gate.Write(item, this.hashset.Add);
        }

        void ICollection<T>.Add(T item)
        {
            _ = this.Add(item);
        }

        public void Clear()
        {
            this.gate.Write(this.hashset.Clear);
        }

        public object Clone()
        {
            return new ConcurrentHashSet<T>(this);
        }

        public bool Contains(T item)
        {
            return this.gate.Read(item, this.hashset.Contains);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.gate.Write(array, arrayIndex, this.hashset.CopyTo);
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            this.gate.Write(other, this.hashset.ExceptWith);
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            this.gate.Write(other, this.hashset.IntersectWith);
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.IsProperSubsetOf);
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.IsProperSupersetOf);
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.IsSubsetOf);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.IsSupersetOf);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.Overlaps);
        }

        public bool Remove(T item)
        {
            return this.gate.Write(item, this.hashset.Remove);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            this.gate.Write(other, this.hashset.UnionWith);
        }

        public IEnumerable<T> Except(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.Except);
        }

        public bool SetEquals(IEnumerable<T> other)
        {
            return this.gate.Read(other, this.hashset.SetEquals);
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            this.gate.Write(other, this.hashset.SymmetricExceptWith);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.gate.Read(this.InternalGetEnumerator);
        }

        private IEnumerator<T> InternalGetEnumerator()
        {
            foreach (var item in this.hashset)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
