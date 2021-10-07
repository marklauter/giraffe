using Documents;
using Documents.Collections;
using Graphs.Incidence;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    public sealed class DocumentIncidenceListSource<TId>
        : IIncidenceListSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<IncidenceListState<TId>> collection;

        public DocumentIncidenceListSource(IDocumentCollection<IncidenceListState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<IMutableIncidenceList<TId>> ReadAsync(TId id)
        {
            var document = await this.collection.ReadAsync(KeyBuilder.GetKey(id));
            var state = document.Member;
            return new IncidenceList<TId>(state.Id, state.Edges);
        }
    }
}
