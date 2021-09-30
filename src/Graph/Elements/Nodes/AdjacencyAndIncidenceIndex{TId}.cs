using Collections.Concurrent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Graphs.Elements
{
    internal sealed class AdjacencyAndIncidenceIndex<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly object gate = new();

        private readonly ConcurrentHashSet<TId> edges;

        internal readonly ConcurrentDictionary<TId, int> nodes;

        public static AdjacencyAndIncidenceIndex<TId> Empty => new();


        public IEnumerable<TId> Edges => this.edges;


        public int EdgeCount => this.edges.Count;


        public bool IsEmpty => this.edges.IsEmpty;


        public IEnumerable<TId> Nodes => this.nodes.Keys;


        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.nodes;


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

        internal AdjacencyAndIncidenceIndex(
            IEnumerable<KeyValuePair<TId, int>> nodes,
            IEnumerable<TId> edges)
        {
            this.nodes = new(nodes);
            this.edges = new(edges);
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


        public object Clone()
        {
            return new AdjacencyAndIncidenceIndex<TId>(this);
        }


        public bool ContainsNode(TId id)
        {
            return this.nodes.ContainsKey(id);
        }


        public bool ContainsEdge(TId id)
        {
            return this.edges.Contains(id);
        }


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
