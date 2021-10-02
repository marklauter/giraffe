using System;
using System.Collections.Immutable;

namespace Graphs.Connections
{
    public sealed partial class Neighbors<TId>
        : IAdjacencyList<TId>
        , IConnectable<TId>
        , IConnectionEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableDictionary<TId, int> neighbors = ImmutableDictionary<TId, int>.Empty;

        public event EventHandler<ConnectedEventArgs<TId>> Connected;
        public event EventHandler<DisconnectedEventArgs<TId>> Disconnected;

        public int Count => this.neighbors.Count;
        
        public TId Id { get; }

        public bool IsEmpty => this.neighbors.IsEmpty;
        
        public int Degree => this.Count;

        public void Connect(TId id)
        {
            var referenceCount = this.ReferenceCount(id) + 1;
            this.neighbors = this.neighbors.SetItem(id, referenceCount);
            this.Connected?.Invoke(this, new ConnectedEventArgs<TId>(this.Id, id));
        }

        public void Disconnect(TId id)
        {
            var referenceCount = this.ReferenceCount(id) - 1;
            this.neighbors = referenceCount == 0
                ? this.neighbors.Remove(id)
                : this.neighbors.SetItem(id, referenceCount);
            this.Disconnected?.Invoke(this, new DisconnectedEventArgs<TId>(this.Id, id));
        }

        public bool IsAdjacent(TId nodeId)
        {
            return this.IsConnected(nodeId);
        }

        public bool IsConnected(TId id)
        {
            return this.neighbors.ContainsKey(id);
        }

        public int ReferenceCount(TId nodeId)
        {
            return this.neighbors.TryGetValue(id, out var count)
                ? count
                : 0;
        }
   }
}
