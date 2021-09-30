using System;

namespace Graphs.Events
{
    public sealed class DeclassifiedEventArgs<TId>
        : ClassifiedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DeclassifiedEventArgs(string label, TId id) : base(label, id)
        {
        }
    }
}
