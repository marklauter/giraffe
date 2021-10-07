using Documents;
using Documents.Collections;
using Graphs.Adjacency;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Output
{
    public sealed class DocumentAdjacencyListSink<TId>
        : IAdjancencyListSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<AdjacencyListState<TId>> collection;

        public DocumentAdjacencyListSink(IDocumentCollection<AdjacencyListState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Task RemoveAsync(TId elementId)
        {
            return this.collection.RemoveAsync(KeyBuilder.GetKey(elementId));
        }

        public async Task WriteAsync(IAdjancencyList<TId> component)
        {
            var state = new AdjacencyListState<TId>(component);
            var document = (Document<AdjacencyListState<TId>>)state;
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
