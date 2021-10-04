using System;

namespace Graphs.Connections
{
    public interface IConnectable<TId>
        : IConnected<TId>
        , IConnectionEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        void Connect(TId id);

        void Disconnect(TId id);
    }
}
