using Graphs.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Connections
{
    public interface IConnectable<TId>
        : IIdentifiable<TId>
        , IEnumerable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        bool IsEmpty { get; }

        int Count { get; }

        void Connect(TId id);

        void Disconnect(TId id);

        bool IsConnected(TId id);
    }
}
