using System;
using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface IEdge<TId>
        : IMutableElement<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsDirected { get; }

        TId SourceId { get; }

        TId TargetId { get; }

        IEnumerable<TId> Nodes { get; }
    }
}
