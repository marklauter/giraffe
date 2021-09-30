using System;

namespace Graphs.Events
{
    public sealed class DisqualifiedEventArgs<TId>
        : QualifiedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisqualifiedEventArgs(string name, object value, TId id)
            : base(name, value, id)
        {
        }
    }
}
