using Documents;
using Documents.Collections;
using Graphs.Classifications;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Output
{
    public sealed class DocumentClassifiedSink<TId>
        : IClassifiedSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<ClassifiedState<TId>> collection;

        public DocumentClassifiedSink(IDocumentCollection<ClassifiedState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Task RemoveAsync(TId elementId)
        {
            return this.collection.RemoveAsync(KeyBuilder.GetKey(elementId));
        }

        public async Task WriteAsync(IClassified<TId> component)
        {
            var state = new ClassifiedState<TId>(component);
            var document = (Document<ClassifiedState<TId>>)state;
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
