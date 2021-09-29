using Graphs.Elements.Edges;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements.Nodes
{
    public class DisconnectedEventArgs<TId>
        : EventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisconnectedEventArgs([DisallowNull] Node<TId> source, [DisallowNull] Edge<TId> edge)
        {
            this.Edge = edge ?? throw new ArgumentNullException(nameof(edge));
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public Edge<TId> Edge { get; }
        
        public Node<TId> Source { get; }
    }
}
