using Documents.Collections;
using Graphs.Elements;
using System;

namespace Graphs.Data
{
    public sealed class DocumentContext<TId>
        : IDocumentContext<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        public IDocumentCollection<Edge<TId>> Edges { get; }
        public IIdGenerator<TId> IdGenerator { get; }
        public IDocumentCollection<Node<TId>> Nodes { get; }

        public DocumentContext(
            IIdGenerator<TId> idGenerator,
            IDocumentCollection<Node<TId>> nodes,
            IDocumentCollection<Edge<TId>> edges)
        {
            this.Edges = edges ?? throw new ArgumentNullException(nameof(edges));
            this.IdGenerator = idGenerator ?? throw new ArgumentNullException(nameof(idGenerator));
            this.Nodes = nodes ?? throw new ArgumentNullException(nameof(nodes));
        }
    }
}
