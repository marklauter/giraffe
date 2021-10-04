using System;

namespace Graphs.Connections
{
    public sealed class DisconnectedEventArgs<TId>
        : ConnectedEventArgs<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public DisconnectedEventArgs(TId sourceId, TId targetId)
            : base(sourceId, targetId)
        {
        }
    }
}
