using System;

namespace Graphs.Elements.Qualifiers
{
    public interface IQualifierEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<QualifiedEventArgs<TId>> Qualified;
        event EventHandler<DisqualifiedEventArgs<TId>> Disqualified;
    }
}
