using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graphs.Nodes
{
    internal sealed class AdjacencyIndex<TId>
        : ICloneable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private ImmutableHashSet<TId> edges = ImmutableHashSet<TId>.Empty;
        private ImmutableDictionary<TId, int> nodes = ImmutableDictionary<TId, int>.Empty;

        public static AdjacencyIndex<TId> Empty => new();

        public IEnumerable<TId> Edges => this.edges;

        public int EdgeCount => this.edges.Count;

        public bool IsEmpty => this.edges.IsEmpty;

        public IEnumerable<TId> Nodes => this.nodes.Keys;

        public int NodeCount => this.nodes.Count;

        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.nodes;

        private AdjacencyIndex() { }

        private AdjacencyIndex(AdjacencyIndex<TId> other)
        {
            this.edges = other.edges;
            this.nodes = other.nodes;
        }

        internal AdjacencyIndex(
            IEnumerable<TId> edges,
            IEnumerable<KeyValuePair<TId, int>> nodes)
        {
            this.edges = edges.ToImmutableHashSet();
            this.nodes = nodes.ToImmutableDictionary();
        }

        public void Add(TId edgeId, TId nodeId)
        {
            this.edges = this.edges.Add(edgeId);

            var referenceCount = this.ReferenceCount(nodeId) + 1;
            this.nodes = this.nodes.SetItem(nodeId, referenceCount);
        }

        public object Clone()
        {
            return new AdjacencyIndex<TId>(this);
        }

        public bool ContainsNode(TId nodeId)
        {
            return this.nodes.ContainsKey(nodeId);
        }

        public bool ContainsEdge(TId edgeId)
        {
            return this.edges.Contains(edgeId);
        }

        public int ReferenceCount(TId nodeId)
        {
            return this.nodes.TryGetValue(nodeId, out var count)
                ? count
                : 0;
        }

        public void Remove(TId edgeId, TId nodeId)
        {
            this.edges = this.edges.Remove(edgeId);

            var referenceCount = this.ReferenceCount(nodeId);
            this.nodes = referenceCount == 1
                ? this.nodes.Remove(nodeId)
                : this.nodes.SetItem(nodeId, referenceCount - 1);
        }
    }
}
