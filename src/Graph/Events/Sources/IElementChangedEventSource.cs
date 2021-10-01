using System;

namespace Graphs.Events
{
    public interface IElementChangedEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<ElementChangedEventArgs<TId>> ElementChanged;
    }
}
