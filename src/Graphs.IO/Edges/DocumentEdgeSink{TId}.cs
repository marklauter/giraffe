using Documents;
using Documents.Collections;
using Graphs.Edges;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Output
{
    public sealed class DocumentEdgeSink<TId>
        : IEdgeSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<EdgeState<TId>> collection;

        public DocumentEdgeSink(IDocumentCollection<EdgeState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Task RemoveAsync(TId elementId)
        {
            return this.collection.RemoveAsync(KeyBuilder.GetKey(elementId));
        }

        public async Task WriteAsync(IEdge<TId> component)
        {
            var state = new EdgeState<TId>(component);
            var document = (Document<EdgeState<TId>>)state;
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
