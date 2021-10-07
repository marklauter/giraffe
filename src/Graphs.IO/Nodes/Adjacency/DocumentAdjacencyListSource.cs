using Documents;
using Documents.Collections;
using Graphs.Adjacency;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    public sealed class DocumentAdjacencyListSource<TId>
        : IAdjacencyListSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<AdjacencyListState<TId>> collection;

        public DocumentAdjacencyListSource(IDocumentCollection<AdjacencyListState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<IMutableAdjacencyList<TId>> ReadAsync(TId id)
        {
            var document = await this.collection.ReadAsync(KeyBuilder.GetKey(id));
            var state = document.Member;
            return new AdjacencyList<TId>(state.Id, state.ReferenceCountedNeighbors);
        }
    }
}
