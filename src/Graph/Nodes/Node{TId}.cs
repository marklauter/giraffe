using Graphs.Identifiers;
using System;
using System.Diagnostics;

namespace Graphs.Nodes
{
    public interface INode<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsAdjacent(INode<TId> node);
        bool IsIncident(IEdge<TId> node);
    }

    [DebuggerDisplay("{Id}, Deg: {Degree}")]
    public sealed class Node<TId>
        : INode<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
