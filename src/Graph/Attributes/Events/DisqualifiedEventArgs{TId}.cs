using System;

namespace Graphs.Attributes
{
    public sealed class DisqualifiedEventArgs<TId>
        : QualifiableEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisqualifiedEventArgs(TId elementId, string name)
            : base(elementId, name)
        {
        }
    }
}
