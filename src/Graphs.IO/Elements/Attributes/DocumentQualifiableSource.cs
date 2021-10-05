using Documents;
using Documents.Collections;
using Graphs.Attributes;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Input
{
    public sealed class DocumentQualifiableSource<TId>
        : IQualifiableSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<QualifiedState<TId>> collection;

        public DocumentQualifiableSource(IDocumentCollection<QualifiedState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task<IQualifiable<TId>> ReadAsync(TId id)
        {
            var document = await this.collection.ReadAsync(KeyBuilder.GetKey(id));
            var state = document.Member;
            return new Qualifiable<TId>(state.Id, state.Attributes);
        }
    }
}
