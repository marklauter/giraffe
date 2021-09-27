using Documents;
using Documents.Collections;
using Graphs.Elements;
using Graphs.Elements.Edges;
using Graphs.Elements.Nodes;
using System;
using System.Threading.Tasks;

namespace Graphs.IO
{
    public sealed class ElementSource<TId>
        : IElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<NodeEntry<TId>> nodeCollection;
        private readonly IDocumentCollection<Edge<TId>> edgeCollection;

        public ElementSource(
            IDocumentCollection<NodeEntry<TId>> nodeCollection,
            IDocumentCollection<Edge<TId>> edgeCollection)
        {
            this.nodeCollection = nodeCollection ?? throw new ArgumentNullException(nameof(nodeCollection));
            this.edgeCollection = edgeCollection ?? throw new ArgumentNullException(nameof(edgeCollection));
        }

        public async Task<Edge<TId>> GetEdgeAsync(TId id)
        {
            var document = await this.edgeCollection.ReadAsync(KeyBuilder.GetKey(id));
            return document.Member;
        }

        public async Task<Node<TId>> GetNodeAsync(TId id)
        {
            var document = await this.nodeCollection.ReadAsync(KeyBuilder.GetKey(id));
            return (Node<TId>)document.Member;
        }
    }
}
