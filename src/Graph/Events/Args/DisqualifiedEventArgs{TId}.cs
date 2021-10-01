using Graphs.Elements;
using System;

namespace Graphs.Events
{
    public sealed class DisqualifiedEventArgs<TId>
        : QualifiedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisqualifiedEventArgs(Element<TId> element, string name, object value)
            : base(element, name, value)
        {
        }
    }
}
