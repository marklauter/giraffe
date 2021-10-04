using System;

namespace Graphs.Classifications
{
    public interface IClassifiableEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<ClassifiedEventArgs<TId>> Classified;

        event EventHandler<DeclassifiedEventArgs<TId>> Declassified;
    }
}
