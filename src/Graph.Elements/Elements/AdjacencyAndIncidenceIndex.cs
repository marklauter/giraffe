using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Contracts;

namespace Graph.Elements
{
    internal sealed class AdjacencyAndIncidenceIndex
        : ICloneable
    {
        private readonly object gate = new();

        [JsonProperty("incidentEdges")]
        private ImmutableHashSet<Guid> edges = ImmutableHashSet<Guid>.Empty;

        [JsonProperty("adjacentNodes")]
        private ImmutableDictionary<Guid, int> nodes = ImmutableDictionary<Guid, int>.Empty;

        public static AdjacencyAndIncidenceIndex Empty => new();

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Edges => this.edges;

        [Pure]
        [JsonIgnore]
        public int EdgeCount => this.edges.Count;

        [Pure]
        [JsonIgnore]
        public bool IsEmpty => this.edges.IsEmpty;

        [Pure]
        [JsonIgnore]
        public IEnumerable<Guid> Nodes => this.nodes.Keys;

        [Pure]
        [JsonIgnore]
        public int NodeCount => this.nodes.Count;

        private AdjacencyAndIncidenceIndex() { }

        private AdjacencyAndIncidenceIndex(AdjacencyAndIncidenceIndex other)
        {
            this.edges = other.edges;
            this.nodes = other.nodes;
        }

        public AdjacencyAndIncidenceIndex Add(Guid edgeId, Guid nodeId)
        {
            lock (this.gate)
            {
                if (!this.edges.Contains(edgeId))
                {
                    this.edges = this.edges
                        .Add(edgeId);

                    var referenceCount = this.nodes.TryGetValue(nodeId, out var count)
                        ? count + 1
                        : 1;

                    this.nodes = this.nodes
                        .SetItem(nodeId, referenceCount);
                }
            }

            return this;
        }

        [Pure]
        public object Clone()
        {
            return new AdjacencyAndIncidenceIndex(this);
        }

        [Pure]
        public bool ContainsNode(Guid id)
        {
            return this.nodes.ContainsKey(id);
        }

        [Pure]
        public bool ContainsEdge(Guid id)
        {
            return this.edges.Contains(id);
        }

        [Pure]
        public int NodeReferenceCount(Guid nodeId)
        {
            return this.nodes.TryGetValue(nodeId, out var count)
                ? count
                : 0;
        }

        public AdjacencyAndIncidenceIndex Remove(Guid edgeId, Guid nodeId)
        {
            if (!this.edges.Contains(edgeId))
            {
                throw new KeyNotFoundException($"{nameof(edgeId)} with value '{edgeId}' is not contained by the index.");
            }

            lock (this.gate)
            {
                this.edges = this.edges
                    .Remove(edgeId);

                var referenceCount = this.nodes.TryGetValue(nodeId, out var count)
                    ? count - 1
                    : 0;

                this.nodes = referenceCount <= 0
                    ? this.nodes.Remove(nodeId)
                    : this.nodes.SetItem(nodeId, referenceCount);
            }

            return this;
        }
    }
}
