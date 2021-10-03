using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Graphs.Edges
{
    [DebuggerDisplay("{Id}, ({SourceId}, {TargetId}) Dir: {IsDirected}")]
    public sealed partial class Edge<TId>
        : IEdge<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IEnumerable<TId> Endpoints
        {
            get
            {
                yield return this.SourceId;
                yield return this.TargetId;
            }
        }

        public TId Id { get; }

        public bool IsDirected { get; }

        public TId SourceId { get; }

        public TId TargetId { get; }

        public bool IsIncident(TId nodeId)
        {
            return this.SourceId.Equals(nodeId) || this.TargetId.Equals(nodeId);
        }
    }
}
