﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace Graphs.Nodes
{
    [DebuggerDisplay("{Id}, Deg: {Degree}")]
    public sealed partial class Node<TId>
        : INode<TId>
        , IConnectionEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableDictionary<TId, int> neighbors = ImmutableDictionary<TId, int>.Empty;

        public event EventHandler<ConnectedEventArgs<TId>> Connected;
        public event EventHandler<DisconnectedEventArgs<TId>> Disconnected;

        public int Count => this.neighbors.Count;

        public int Degree => this.Count;

        public TId Id { get; }

        public IEnumerable<TId> Neighbors => this.neighbors.Keys;

        public bool IsEmpty => this.neighbors.IsEmpty;

        public void Connect(TId id)
        {
            var referenceCount = this.NeighborReferenceCount(id) + 1;
            this.neighbors = this.neighbors.SetItem(id, referenceCount);
            this.Connected?.Invoke(this, new ConnectedEventArgs<TId>(this.Id, id));
        }

        public void Disconnect(TId id)
        {
            var referenceCount = this.NeighborReferenceCount(id) - 1;
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

        public int NeighborReferenceCount(TId nodeId)
        {
            return this.neighbors.TryGetValue(nodeId, out var count)
                ? count
                : 0;
        }
    }
}
