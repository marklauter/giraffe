using Documents;
using Documents.Collections;
using Graphs.Incidence;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Output
{
    public sealed class DocumentIncidenceListSink<TId>
        : IIncidenceListSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<IncidenceListState<TId>> collection;

        public DocumentIncidenceListSink(IDocumentCollection<IncidenceListState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Task RemoveAsync(TId elementId)
        {
            return this.collection.RemoveAsync(KeyBuilder.GetKey(elementId));
        }

        public async Task WriteAsync(IIncidenceList<TId> component)
        {
            var state = new IncidenceListState<TId>(component);
            var document = (Document<IncidenceListState<TId>>)state;
            if (await this.collection.ContainsAsync(KeyBuilder.GetKey(component.Id)))
            {
                await this.collection.UpdateAsync(document);
            }
            else
            {
                await this.collection.AddAsync(document);
            }
        }
    }
}
