using Graphs.Elements.Edges;
using Graphs.Elements.Nodes;
using System;

namespace Graphs.Elements
{
    public interface IConnectionEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<QualifiedEventArgs<TId>> Qualified;
        event EventHandler<DisqualifiedEventArgs<TId>> Disqualified;
    }

    public interface IConnector<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        Edge<TId> Connect(Node<TId> source, Node<TId> Target);
    }
}
