using Documents;
using Documents.Collections;
using Graphs.Edges;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    public sealed class DocumentEdgeSource<TId>
        : IEdgeSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<EdgeState<TId>> collection;

        public DocumentEdgeSource(IDocumentCollection<EdgeState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<IEdge<TId>> ReadAsync(TId id)
        {
            var document = await this.collection.ReadAsync(KeyBuilder.GetKey(id));
            var state = document.Member;
            return new Edge<TId>(state.Id, state.SourceId, state.TargetId, state.IsDirected);
        }
    }
}
