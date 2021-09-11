using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Graph.Elements
{
    [JsonArray]
    internal sealed class AdjacencyAndIncidenceIndex
        : IEnumerable<KeyValuePair<Guid, ImmutableHashSet<Guid>>>
        , ICloneable
    {
        private readonly object gate = new();
#pragma warning disable IDE0032 // Use auto property
        private ImmutableHashSet<Guid> edges = ImmutableHashSet<Guid>.Empty;
        private ImmutableHashSet<Guid> nodes = ImmutableHashSet<Guid>.Empty;
#pragma warning restore IDE0032 // Use auto property
        private ImmutableDictionary<Guid, ImmutableHashSet<Guid>> nodesToEdges = ImmutableDictionary<Guid, ImmutableHashSet<Guid>>.Empty;

        public static AdjacencyAndIncidenceIndex Empty => new();

        [JsonIgnore]
        public int Count => this.nodes.Count;

        [JsonIgnore]
        public ImmutableHashSet<Guid> Edges => this.edges;

        [JsonIgnore]
        public ImmutableHashSet<Guid> Nodes => this.nodes;

        private AdjacencyAndIncidenceIndex() { }

        private AdjacencyAndIncidenceIndex(AdjacencyAndIncidenceIndex other)
        {
            this.edges = other.edges;
            this.nodes = other.nodes;
            this.nodesToEdges = other.nodesToEdges;
        }

        [JsonConstructor]
        [SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Used for serialization.")]
        private AdjacencyAndIncidenceIndex(ImmutableDictionary<Guid, ImmutableHashSet<Guid>> nodesAndEdges)
        {
            this.nodesToEdges = this.nodesToEdges
                .AddRange(nodesAndEdges);

            this.nodes = this.nodes
                .Union(this.nodesToEdges.Keys);

            var allEdges = nodesAndEdges
                .SelectMany(te => te.Value)
                .Distinct();

            this.edges = this.nodes
                .Union(allEdges);
        }

        public void Add(Edge edge, Guid nodeId)
        {
            lock (this.gate)
            {
                this.edges = this.edges
                    .Add(edge.Id);

                this.nodes = this.nodes
                    .Add(nodeId);

                if (!this.nodesToEdges.TryGetValue(nodeId, out var edges))
                {
                    edges = ImmutableHashSet<Guid>.Empty;
                }

                edges = edges.Add(edge.Id);
                this.nodesToEdges = this.nodesToEdges
                    .SetItem(nodeId, edges);
            }
        }

        public void Remove(Edge edge, Guid nodeId)
        {
            lock (this.gate)
            {
                this.edges = this.edges
                    .Remove(edge.Id);

                if (this.nodesToEdges.TryGetValue(nodeId, out var edges))
                {
                    edges = edges
                        .Remove(edge.Id);

                    // only remove a node when it has more incident edges
                    if (edges.IsEmpty)
                    {
                        this.nodes = this.nodes
                            .Remove(nodeId);

                        this.nodesToEdges = this.nodesToEdges
                            .Remove(nodeId);
                    }
                    else
                    {
                        this.nodesToEdges = this.nodesToEdges
                            .SetItem(nodeId, edges);
                    }
                }
            }
        }

        public object Clone()
        {
            return new AdjacencyAndIncidenceIndex(this);
        }

        public bool ContainsNode(Guid id)
        {
            return this.nodes.Contains(id);
        }

        public bool ContainsEdge(Guid id)
        {
            return this.edges.Contains(id);
        }

        public IEnumerator<KeyValuePair<Guid, ImmutableHashSet<Guid>>> GetEnumerator()
        {
            foreach (var kvp in this.nodesToEdges)
            {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
