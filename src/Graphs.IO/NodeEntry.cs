using Graphs.Elements.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Graphs.IO
{
    [JsonObject("node")]
    public sealed class NodeEntry<TId>
        : ElementEntry<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly Node<TId> node;

        [JsonProperty("edges")]
        public IEnumerable<TId> Edges => this.node.Edges;

        [JsonProperty("nodes")]
        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.node.ReferenceCountedNodes;

        private NodeEntry(Node<TId> node)
            : base(node)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }

        [JsonConstructor]
        internal NodeEntry(
            TId id,
            [DisallowNull, Pure] IEnumerable<string> classifications,
            [DisallowNull, Pure] IEnumerable<KeyValuePair<string, object>> qualifications,
            [DisallowNull, Pure] IEnumerable<KeyValuePair<TId, int>> nodes,
            [DisallowNull, Pure] IEnumerable<TId> edges)
        {
            this.node = new Node<TId>(
                id,
                classifications,
                qualifications,
                nodes,
                edges);
        }

        public static explicit operator Node<TId>(NodeEntry<TId> nodeEntry)
        {
            return nodeEntry.node;
        }

        public static explicit operator NodeEntry<TId>([DisallowNull, Pure] Node<TId> node)
        {
            return new NodeEntry<TId>(node);
        }
    }
}
