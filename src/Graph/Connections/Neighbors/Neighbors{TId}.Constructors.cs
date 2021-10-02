using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Connections
{
    public sealed partial class Neighbors<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public Neighbors(TId id)
        {
            this.Id = id;
        }

        public Neighbors(IEnumerable<KeyValuePair<TId, int>> neighbors)
        {
            this.neighbors = neighbors.ToImmutableDictionary();
        }

        private Neighbors(Neighbors<TId> other)
        {
            this.neighbors = other.neighbors;
        }

        public object Clone()
        {
            return new Neighbors<TId>(this);
        }
    }
}
