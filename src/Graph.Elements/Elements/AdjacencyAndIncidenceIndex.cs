using Collections.Concurrent;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Graphs.Elements
{
    internal sealed class AdjacencyAndIncidenceIndex
        : ICloneable
    {
        private readonly object gate = new();

        [JsonProperty("incidentEdges")]
        private readonly ConcurrentHashSet<Guid> edges;

        [JsonProperty("adjacentNodes")]
        private readonly ConcurrentDictionary<Guid, int> nodes;

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

        private AdjacencyAndIncidenceIndex()
        {
            this.edges = ConcurrentHashSet<Guid>.Empty;
            this.nodes = new();
        }

        private AdjacencyAndIncidenceIndex(AdjacencyAndIncidenceIndex other)
        {
            this.edges = new(other.edges);
            this.nodes = new(other.nodes);
        }

        public AdjacencyAndIncidenceIndex Add(Guid edgeId, Guid nodeId)
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
