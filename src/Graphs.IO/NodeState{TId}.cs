using Graphs.Elements.Nodes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Graphs.IO
{
    [JsonObject("node")]
    public sealed class NodeState<TId>
        : ElementState<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly Node<TId> node;

        [JsonProperty("edges")]
        public IEnumerable<TId> Edges => this.node.Edges;

        [JsonProperty("nodes")]
        public IEnumerable<KeyValuePair<TId, int>> ReferenceCountedNodes => this.node.ReferenceCountedNodes;

        private NodeState(Node<TId> node)
            : base(node)
        {
            this.node = node ?? throw new ArgumentNullException(nameof(node));
        }

        [JsonConstructor]
        internal NodeState(
            TId id,
            IEnumerable<string> classifications,
            IEnumerable<KeyValuePair<string, object>> qualifications,
            IEnumerable<KeyValuePair<TId, int>> nodes,
            IEnumerable<TId> edges)
        {
            this.node = new Node<TId>(
                id,
                classifications,
                qualifications,
                nodes,
                edges);
        }

        public static explicit operator NodeState<TId>(Node<TId> node)
        {
            return new NodeState<TId>(node);
        }

        public static explicit operator Node<TId>(NodeState<TId> nodeEntry)
        {
            return nodeEntry.node;
        }
    }
}
