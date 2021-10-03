using System;

namespace Graphs.Edges
{
    public sealed partial class Edge<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public Edge(TId id, TId sourceId, TId targetId, bool isDirected)
        {
            this.Id = id;
            this.SourceId = sourceId;
            this.TargetId = targetId;
            this.IsDirected = isDirected;
        }

        private Edge(Edge<TId> other)
            : this(other.Id, other.SourceId, other.TargetId, other.IsDirected)
        {
        }

        public object Clone()
        {
            return new Edge<TId>(this);
        }
    }
}
