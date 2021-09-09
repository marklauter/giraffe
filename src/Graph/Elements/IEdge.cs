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
        Guid SourceId { get; }
        Guid TargetId { get; }

        IEnumerable<Guid> Nodes();
    }
}
