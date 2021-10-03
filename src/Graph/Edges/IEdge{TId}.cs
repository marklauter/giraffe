using Graphs.Attributes;
using Graphs.Classes;
using System;

namespace Graphs.Edges
{
    public interface IEdge<TId>
        : IClassified<TId>
        , IQualified<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsDirected { get; }

        TId SourceId { get; }

        TId TargetId { get; }
    }
}
