using Documents;
using Documents.Collections;
using Graphs.Elements;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    // todo: Graphs.IO is the place to put the element collection implementation that uses the document colletion beind the scenes
    public sealed class ElementSource<TId>
        : IElementSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<NodeState<TId>> nodeCollection;
        private readonly IDocumentCollection<EdgeState<TId>> edgeCollection;

        public ElementSource(
            IDocumentCollection<NodeState<TId>> nodeCollection,
            IDocumentCollection<EdgeState<TId>> edgeCollection)
        {
            this.nodeCollection = nodeCollection ?? throw new ArgumentNullException(nameof(nodeCollection));
            this.edgeCollection = edgeCollection ?? throw new ArgumentNullException(nameof(edgeCollection));
        }

        public async Task<Edge<TId>> GetEdgeAsync(TId id)
        {
            var document = await this.edgeCollection.ReadAsync(KeyBuilder.GetKey(id));
            return (Edge<TId>)document.Member;
        }

        public async Task<Node<TId>> GetNodeAsync(TId id)
        {
            var document = await this.nodeCollection.ReadAsync(KeyBuilder.GetKey(id));
            return (Node<TId>)document.Member;
        }
    }
}
