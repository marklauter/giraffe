using Documents;
using Documents.Collections;
using Graphs.Classifications;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    public sealed class DocumentClassifiableSource<TId>
        : IClassifiableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<ClassifiedState<TId>> collection;

        public DocumentClassifiableSource(IDocumentCollection<ClassifiedState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<IClassifiable<TId>> ReadAsync(TId id)
        {
            var document = await this.collection.ReadAsync(KeyBuilder.GetKey(id));
            var state = document.Member;
            return new Classifiable<TId>(state.Id, state.Labels);
        }
    }
}
