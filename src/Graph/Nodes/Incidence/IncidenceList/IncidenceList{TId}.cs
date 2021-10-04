using Graphs.Connections;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Incidence
{
    public sealed partial class IncidenceList<TId>
        : IMutableIncidenceList<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableHashSet<TId> edges = ImmutableHashSet<TId>.Empty;

        public event EventHandler<ConnectedEventArgs<TId>> Connected;
        public event EventHandler<DisconnectedEventArgs<TId>> Disconnected;

        public int Count => this.edges.Count;

        public IEnumerable<TId> Edges => this.edges;

        /// <summary>
        /// Node Id
        /// </summary>
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
