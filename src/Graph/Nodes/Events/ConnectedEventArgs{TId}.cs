using Graphs.Elements;
using Graphs.Events;
using System;

namespace Graphs.Nodes
{
    public class ConnectedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ConnectedEventArgs(Node<TId> source, Node<TId> target, Edge<TId> edge)
            : base()
        {
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
            this.Target = target ?? throw new ArgumentNullException(nameof(target));
            this.Edge = edge ?? throw new ArgumentNullException(nameof(edge));
        }

        public Node<TId> Source { get; }

        public Node<TId> Target { get; }

        public Edge<TId> Edge { get; }
    }
}
