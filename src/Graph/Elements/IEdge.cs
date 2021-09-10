using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    public interface IEdge
        : IElement<Guid>
        , IEquatable<IEdge>
        , IEqualityComparer<IEdge>
    {
        bool IsDirected { get; }
        IEnumerable<Guid> Nodes { get; }
        Guid SourceId { get; }
        Guid TargetId { get; }
    }
}
