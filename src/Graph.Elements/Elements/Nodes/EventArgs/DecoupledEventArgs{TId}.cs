using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements
{
    public class DecoupledEventArgs<TId>
        : EventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DecoupledEventArgs([DisallowNull] INode<TId> source, [DisallowNull] IEdge<TId> edge)
        {
            this.Edge = edge ?? throw new ArgumentNullException(nameof(edge));
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IEdge<TId> Edge { get; }
        public INode<TId> Source { get; }
    }
}
