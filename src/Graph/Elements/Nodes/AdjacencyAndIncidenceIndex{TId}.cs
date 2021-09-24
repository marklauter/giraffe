using Collections.Concurrent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Graphs.Elements.Nodes
{
    internal sealed class AdjacencyAndIncidenceIndex<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly object gate = new();

        private readonly ConcurrentHashSet<TId> edges;

        private readonly ConcurrentDictionary<TId, int> nodes;

        public static AdjacencyAndIncidenceIndex<TId> Empty => new();

        [Pure]
        public IEnumerable<TId> Edges => this.edges;

        [Pure]
        public int EdgeCount => this.edges.Count;

        [Pure]
        public bool IsEmpty => this.edges.IsEmpty;

        [Pure]
        public IEnumerable<TId> Nodes => this.nodes.Keys;

        [Pure]
        public int NodeCount => this.nodes.Count;

        private AdjacencyAndIncidenceIndex()
        {
            this.edges = ConcurrentHashSet<TId>.Empty;
            this.nodes = new();
        }

        private AdjacencyAndIncidenceIndex(AdjacencyAndIncidenceIndex<TId> other)
        {
            this.edges = new(other.edges);
            this.nodes = new(other.nodes);
        }

        public AdjacencyAndIncidenceIndex<TId> Add(TId edgeId, TId nodeId)
        {
            lock (this.gate)
            {
                if (!this.edges.Contains(edgeId))
                {
                    _ = this.edges.Add(edgeId);

                    var referenceCount = this.nodes.TryGetValue(nodeId, out var count)
                        ? count + 1
                        : 1;

                    _ = this.nodes[nodeId] = referenceCount;
                }
            }

            return this;
        }

        [Pure]
        public object Clone()
        {
            return new AdjacencyAndIncidenceIndex<TId>(this);
        }

        [Pure]
        public bool ContainsNode(TId id)
        {
            return this.nodes.ContainsKey(id);
        }

        [Pure]
        public bool ContainsEdge(TId id)
        {
            return this.edges.Contains(id);
        }

        [Pure]
        public int NodeReferenceCount(TId nodeId)
        {
            return this.nodes.TryGetValue(nodeId, out var count)
                ? count
                : 0;
        }

        public AdjacencyAndIncidenceIndex<TId> Remove(TId edgeId, TId nodeId)
        {
            lock (this.gate)
            {
                if (!this.edges.Remove(edgeId))
                {
                    throw new KeyNotFoundException($"{nameof(edgeId)} with value '{edgeId}' is not contained by the index.");
                }

                var referenceCount = this.nodes.TryGetValue(nodeId, out var count)
                    ? count - 1
                    : 0;

                if (referenceCount <= 0)
                {
                    _ = this.nodes.TryRemove(nodeId, out var _);
                }
                else
                {
                    this.nodes[nodeId] = referenceCount;
                }
            }

            return this;
        }
    }
}
