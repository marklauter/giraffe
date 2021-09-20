using Documents.Collections;
using Graphs.Elements;
using System;

namespace Graphs.Data
{
    public sealed class GraphDocumentContext<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IDocumentCollection<INode<TId>> Nodes { get; }
        public IDocumentCollection<IEdge<TId>> Edges { get; }

        public GraphDocumentContext(
            IDocumentCollection<INode<TId>> nodes,
            IDocumentCollection<IEdge<TId>> edges)
        {
            this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }
    }
}
