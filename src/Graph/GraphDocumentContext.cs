using Documents.Collections;
using Graphs.Elements;
using System;

namespace Graphs.Data
{
    public sealed class GraphDocumentContext
    {
        public IDocumentCollection<Node> Nodes { get; }
        public IDocumentCollection<Edge> Edges { get; }

        public GraphDocumentContext(
            IDocumentCollection<Node> nodes,
            IDocumentCollection<Edge> edges)
        {
            this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
            this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
        }
    }
}
