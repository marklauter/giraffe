using System;

namespace Graphs.Events
{
    public interface IGraphEventSource<TId>
        : IElementChangedEventSource<TId>
        , IConnectionEventSource<TId>
        , INodeEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
    }
}
