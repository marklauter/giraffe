using System;

namespace Graphs.Events
{
    public interface IClassifierEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<ClassifiedEventArgs<TId>> Classified;

        event EventHandler<DeclassifiedEventArgs<TId>> Declassified;
    }
}
