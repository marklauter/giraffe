using Documents;
using Documents.Collections;
using Graphs.Attributes;
using System;
using System.Threading.Tasks;

namespace Graphs.IO.Output
{
    public sealed class DocumentQualifiedSink<TId>
        : IQualifiedSink<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IDocumentCollection<QualifiedState<TId>> collection;

        public DocumentQualifiedSink(IDocumentCollection<QualifiedState<TId>> collection)
        {
            this.collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public Task RemoveAsync(TId elementId)
        {
            return this.collection.RemoveAsync(KeyBuilder.GetKey(elementId));
        }

        public async Task WriteAsync(IQualified<TId> component)
        {
            var state = new QualifiedState<TId>(component);
            var document = (Document<QualifiedState<TId>>)state;
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
