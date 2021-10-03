using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Edges
{
    public interface IEdge<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        IEnumerable<TId> Endpoints { get; }
        bool IsDirected { get; }
        TId SourceId { get; }
        TId TargetId { get; }

        bool IsIncident(TId nodeId);
    }
}
