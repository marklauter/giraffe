using System;
using System.Collections.Immutable;

namespace Graphs.Connections
{
    public sealed partial class IncidentEdges<TId>
        : IConnectable<TId>
        , IConnectionEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableHashSet<TId> edges = ImmutableHashSet<TId>.Empty;

        public event EventHandler<ConnectedEventArgs<TId>> Connected;
        public event EventHandler<DisconnectedEventArgs<TId>> Disconnected;

        public int Count => this.edges.Count;

        public TId Id { get; }

        public bool IsEmpty => this.edges.IsEmpty;

        public void Connect(TId id)
        {
            this.edges = this.edges.Add(id);
            this.Connected?.Invoke(this, new ConnectedEventArgs<TId>(this.Id, id));
        }

        public void Disconnect(TId id)
        {
            this.edges = this.edges.Remove(id);
            this.Disconnected?.Invoke(this, new DisconnectedEventArgs<TId>(this.Id, id));
        }

        public bool IsConnected(TId id)
        {
            return this.edges.Contains(id);
        }
    }
}
