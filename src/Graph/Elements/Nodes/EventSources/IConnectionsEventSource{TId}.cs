using System;

namespace Graphs.Elements
{
    public interface IConnectionsEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        event EventHandler<ConnectedEventArgs<TId>> Connected;
        event EventHandler<DisconnectedEventArgs<TId>> Disconnected;
    }
}
