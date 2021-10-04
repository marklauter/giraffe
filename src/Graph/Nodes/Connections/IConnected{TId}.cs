using Graphs.Identifiers;
using System;

namespace Graphs.Connections
{
    public interface IConnected<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        int Count { get; }

        bool IsEmpty { get; }

        bool IsConnected(TId id);
    }
}
