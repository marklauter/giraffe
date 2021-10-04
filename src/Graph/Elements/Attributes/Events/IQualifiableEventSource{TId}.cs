using System;

namespace Graphs.Attributes
{
    public interface IQualifiableEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<QualifiedEventArgs<TId>> Qualified;
        event EventHandler<DisqualifiedEventArgs<TId>> Disqualified;
    }
}
