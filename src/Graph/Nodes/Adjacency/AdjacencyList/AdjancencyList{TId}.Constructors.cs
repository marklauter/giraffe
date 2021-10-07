using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Adjacency
{
    public sealed partial class AdjancencyList<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public AdjancencyList(TId id)
        {
            this.Id = id;
        }

        public AdjancencyList(TId id, IEnumerable<KeyValuePair<TId, int>> neighbors)
            : this(id)
        {
            this.neighbors = neighbors.ToImmutableDictionary();
        }

        private AdjancencyList(AdjancencyList<TId> other)
            : this(other.Id)
        {
            this.neighbors = other.neighbors;
        }

        public object Clone()
        {
            return new AdjancencyList<TId>(this);
        }
    }
}
