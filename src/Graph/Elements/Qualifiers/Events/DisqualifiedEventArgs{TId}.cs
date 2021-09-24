using System;

namespace Graphs.Elements.Qualifiers
{
    public sealed class DisqualifiedEventArgs<TId>
        : DisqualifiedEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisqualifiedEventArgs(string name, TId id)
            : base(name)
        {
            this.Id = id;
        }

        public TId Id { get; }
    }
}
