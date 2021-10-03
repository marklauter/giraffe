using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Nodes
{
    public sealed partial class Node<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public Node(TId id)
        {
            this.Id = id;
        }

        public Node(TId id, IEnumerable<KeyValuePair<TId, int>> neighbors)
            : this(id)
        {
            this.neighbors = neighbors.ToImmutableDictionary();
        }

        private Node(Node<TId> other)
            : this(other.Id)
        {
            this.neighbors = other.neighbors;
        }

        public object Clone()
        {
            return new Node<TId>(this);
        }
    }
}
