using Graphs.Events;
using System;

namespace Graphs.Connections
{
    public class ConnectedEventArgs<TId>
        : GraphEventArgs
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public ConnectedEventArgs(TId sourceId, TId targetId)
            : base()
        {
            this.SourceId = sourceId;
            this.TargetId = targetId;
        }

        public TId SourceId { get; }
        public TId TargetId { get; }
    }
}
