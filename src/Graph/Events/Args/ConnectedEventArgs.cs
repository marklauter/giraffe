using System;

namespace Graphs.Events
{
    public class ConnectedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ConnectedEventArgs(Node<TId> source, Node<TId> target, Edge<TId> edge)
            : base()
        {
            this.Source = source;
            this.Target = target;
            this.Edge = edge;
        }

        public Node<TId> Source { get; }

        public Node<TId> Target { get; }

        public Edge<TId> Edge { get; }
    }
}
