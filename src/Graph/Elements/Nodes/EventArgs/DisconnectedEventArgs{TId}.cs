using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements
{
    public class DisconnectedEventArgs<TId>
        : EventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisconnectedEventArgs([DisallowNull] INode<TId> source, [DisallowNull] IEdge<TId> edge)
        {
            this.Edge = edge ?? throw new ArgumentNullException(nameof(edge));
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IEdge<TId> Edge { get; }
        public INode<TId> Source { get; }
    }
}
