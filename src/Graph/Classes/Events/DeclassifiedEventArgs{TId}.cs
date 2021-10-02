using System;

namespace Graphs.Classes
{
    public sealed class DeclassifiedEventArgs<TId>
        : ClassifiedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DeclassifiedEventArgs(TId elementId, string label) : base(elementId, label)
        {
        }
    }
}
