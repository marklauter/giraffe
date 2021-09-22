using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Documents.Collections
{
    /// <inheritdoc/>
    public abstract class DocumentCollection<TMember>
        : IDocumentCollection<TMember>
        where TMember : class
    {
        /// <inheritdoc/>
        public event EventHandler<DocumentAddedEventArgs<TMember>> DocumentAdded;

        /// <inheritdoc/>
        public event EventHandler<DocumentRemovedEventArgs<TMember>> DocumentRemoved;

        /// <inheritdoc/>
        public event EventHandler<DocumentUpdatedEventArgs<TMember>> DocumentUpdated;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> Cleared;

        protected DocumentCollection() { }

        /// <inheritdoc/>
        [Pure]
        public abstract int Count { get; }

        [Pure]
        public bool IsEmpty => this.Count == 0;

        /// <inheritdoc/>
        public Task AddAsync([Pure] Document<TMember> document)
        {
            return document is null
                ? throw new ArgumentNullException(nameof(document))
                : this.InternalAddAsync(document);
        }

        /// <inheritdoc/>
        public Task AddAsync([Pure] IEnumerable<Document<TMember>> documents)
        {
            if (documents is null)
            {
                throw new ArgumentNullException(nameof(documents));
            }

            var tasks = documents.Select(document => this.AddAsync(document));
            return Task.WhenAll(tasks);
        }

        /// <inheritdoc/>
        public async Task ClearAsync()
        {
            await this.ClearCollectionAsync();
            this.Cleared?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        [Pure]
        public Task<bool> ContainsAsync(string key)
        {
            return String.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key))
                : this.ContainsDocumentAsync(key);
        }

        /// <inheritdoc/>
        [Pure]
        public Task<bool> ContainsAsync([Pure] Document<TMember> document)
        {
            return document is null
                ? throw new ArgumentNullException(nameof(document)) :
                this.ContainsAsync(document.Key);
        }

        /// <inheritdoc/>
        [Pure]
        public Task<Document<TMember>> ReadAsync(string key)
        {
            return String.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key))
                : this.ReadDocumentAsync(key);
        }

        /// <inheritdoc/>
        [Pure]
        public Task<IEnumerable<Document<TMember>>> ReadAsync(IEnumerable<string> keys)
        {
            return keys is null
                ? throw new ArgumentNullException(nameof(keys))
                : this.InternalReadAsync(keys);
        }

        /// <inheritdoc/>
        public Task RemoveAsync(string key)
        {
            return String.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key))
                : this.InternalRemoveAsync(key);
        }

        /// <inheritdoc/>
        public Task RemoveAsync(IEnumerable<string> keys)
        {
            return keys is null
                ? throw new ArgumentNullException(nameof(keys))
                : this.InternalRemoveAsync(keys);
        }

        /// <inheritdoc/>
        public Task RemoveAsync([Pure] Document<TMember> document)
        {
            return document is null
                ? throw new ArgumentNullException(nameof(document))
                : this.InternalRemoveAsync(document);
        }

        /// <inheritdoc/>
        public Task RemoveAsync([Pure] IEnumerable<Document<TMember>> documents)
        {
            return documents is null
                ? throw new ArgumentNullException(nameof(documents))
                : this.InternalRemoveAsync(documents);
        }

        /// <inheritdoc/>
        public Task UpdateAsync([Pure] Document<TMember> document)
        {
            return document is null
                ? throw new ArgumentNullException(nameof(document))
                : this.InternalUpdateAsync(document);
        }

        /// <inheritdoc/>
        public Task UpdateAsync([Pure] IEnumerable<Document<TMember>> documents)
        {
            return documents is null
                ? throw new ArgumentNullException(nameof(documents))
                : this.InternalUpdateAsync(documents);
        }

        protected abstract Task ClearCollectionAsync();

        protected abstract Task<bool> ContainsDocumentAsync(string key);

        protected abstract Task<Document<TMember>> ReadDocumentAsync(string key);

        protected abstract Task RemoveDocumentAsync(string key);

        protected abstract Task WriteDocumentAsync([Pure] Document<TMember> document);

        private async Task InternalAddAsync([Pure] Document<TMember> document)
        {
            if (await this.ContainsAsync(document.Key))
            {
                throw new InvalidOperationException($"Document with key '{document.Key}' is already in the collection.");
            }

            await this.WriteDocumentAsync(document);
            this.DocumentAdded?.Invoke(this, new DocumentAddedEventArgs<TMember>(document));
        }

        private async Task<IEnumerable<Document<TMember>>> InternalReadAsync(IEnumerable<string> keys)
        {
            var tasks = keys.Select(key => this.ReadAsync(key));
            return await Task.WhenAll(tasks);
        }

        private async Task InternalRemoveAsync(string key)
        {
            if (await this.ContainsAsync(key))
            {
                var document = await this.ReadAsync(key);
                await this.RemoveDocumentAsync(key);
                this.DocumentRemoved?.Invoke(this, new DocumentRemovedEventArgs<TMember>(document));
            }
        }

        private async Task InternalRemoveAsync(IEnumerable<string> keys)
        {
            var tasks = keys
                .Select(key => this.RemoveAsync(key));

            await Task.WhenAll(tasks);
        }

        private async Task InternalRemoveAsync([Pure] Document<TMember> document)
        {
            await this.RemoveDocumentAsync(document.Key);
            this.DocumentRemoved?.Invoke(this, new DocumentRemovedEventArgs<TMember>(document));
        }

        private async Task InternalRemoveAsync([Pure] IEnumerable<Document<TMember>> documents)
        {
            var tasks = documents
                .Select(document => this.RemoveAsync(document));

            await Task.WhenAll(tasks);
        }

        private async Task InternalUpdateAsync([Pure] Document<TMember> document)
        {
            if (!await this.ContainsAsync(document.Key))
            {
                throw new KeyNotFoundException(document.Key);
            }

            var original = await this.ReadAsync(document.Key);
            if (original.ETag != document.ETag)
            {
                throw new ETagMismatchException($"key: {document.Key}, expected: {original.ETag}, actual: {document.ETag}");
            }

            await this.WriteDocumentAsync(document);
            this.DocumentUpdated?.Invoke(this, new DocumentUpdatedEventArgs<TMember>(document));
        }

        private async Task InternalUpdateAsync([Pure] IEnumerable<Document<TMember>> documents)
        {
            var tasks = documents
                .Select(document => this.UpdateAsync(document));

            await Task.WhenAll(tasks);
        }
    }
}
