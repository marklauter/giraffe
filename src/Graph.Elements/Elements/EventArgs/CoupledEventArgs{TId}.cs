using System;
using System.Diagnostics.CodeAnalysis;

namespace Graphs.Elements
{
    public class CoupledEventArgs<TId>
        : EventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public CoupledEventArgs([DisallowNull] INode<TId> source, [DisallowNull] INode<TId> target, [DisallowNull] IEdge<TId> relationship)
        {
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
            this.Target = target ?? throw new ArgumentNullException(nameof(target));
            this.Relationship = relationship ?? throw new ArgumentNullException(nameof(relationship));
        }

        public INode<TId> Source { get; }
        public INode<TId> Target { get; }
        public IEdge<TId> Relationship { get; }
    }
}
