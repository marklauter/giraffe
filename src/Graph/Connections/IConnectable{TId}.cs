using Graphs.Identifiers;
using System;

namespace Graphs.Connections
{
    public interface IConnectable<TId>
        : IIdentifiable<TId>
        , ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        int Count { get; }

        bool IsEmpty { get; }

        void Connect(TId id);

        void Disconnect(TId id);

        bool IsConnected(TId id);
    }
}
