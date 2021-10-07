using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Adjacency
{
    public sealed partial class AdjacencyList<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public AdjacencyList(TId id)
        {
            this.Id = id;
        }

        public AdjacencyList(TId id, IEnumerable<KeyValuePair<TId, int>> neighbors)
            : this(id)
        {
            this.neighbors = neighbors.ToImmutableDictionary();
        }

        private AdjacencyList(AdjacencyList<TId> other)
            : this(other.Id)
        {
            this.neighbors = other.neighbors;
        }

        public object Clone()
        {
            return new AdjacencyList<TId>(this);
        }
    }
}
