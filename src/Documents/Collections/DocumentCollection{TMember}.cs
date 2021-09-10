using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Documents
{
    /// <inheritdoc/>
    public abstract class DocumentCollection<TMember>
        : IDocumentCollection<TMember>
        where TMember : class
    {
        /// <inheritdoc/>
        public event EventHandler<DocumentAddedEventArgs<TMember>> AfterAdd;

        /// <inheritdoc/>
        public event EventHandler<DocumentRemovedEventArgs<TMember>> AfterRemove;

        /// <inheritdoc/>
        public event EventHandler<DocumentUpdatedEventArgs<TMember>> AfterUpdate;

        /// <inheritdoc/>
        public event EventHandler<EventArgs> Cleared;

        /// <inheritdoc/>
        [Pure]
        public abstract int Count { get; }

        /// <inheritdoc/>
        public void Add([Pure] Document<TMember> document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            this.AddDocument(document);
            this.AfterAdd?.Invoke(this, new DocumentAddedEventArgs<TMember>(document));
        }

        /// <inheritdoc/>
        public void Add([Pure] IEnumerable<Document<TMember>> documents)
        {
            if (documents is null)
            {
                throw new ArgumentNullException(nameof(documents));
            }

            foreach (var document in documents)
            {
                this.Add(document);
            }
        }

        /// <inheritdoc/>
        public void Clear()
        {
            this.ClearCollection();
            this.Cleared?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Contains(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            return this.ContainsDocument(key);
        }

        /// <inheritdoc/>
        [Pure]
        public bool Contains([Pure] Document<TMember> document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            return this.Contains(document.Key);
        }

        /// <inheritdoc/>
        [Pure]
        public Document<TMember> Read(string key)
        {
            return String.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key))
                : this.ReadDocument(key);
        }

        /// <inheritdoc/>
        [Pure]
        public IEnumerable<Document<TMember>> Read(IEnumerable<string> keys)
        {
            return keys is null
                ? throw new ArgumentNullException(nameof(keys))
                : this.ReadDocuments(keys);
        }

        /// <inheritdoc/>
        public void Remove(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            var document = this.Read(key);
            this.RemoveDocument(key);
            this.AfterRemove?.Invoke(this, new DocumentRemovedEventArgs<TMember>(document));
        }

        /// <inheritdoc/>
        public void Remove(IEnumerable<string> keys)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            foreach (var key in keys)
            {
                this.Remove(key);
            }
        }

        /// <inheritdoc/>
        public void Remove([Pure] Document<TMember> document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            this.Remove(document.Key);
        }

        /// <inheritdoc/>
        public void Remove([Pure] IEnumerable<Document<TMember>> documents)
        {
            if (documents is null)
            {
                throw new ArgumentNullException(nameof(documents));
            }

            foreach (var document in documents)
            {
                this.Remove(document);
            }
        }

        /// <inheritdoc/>
        public void Update([Pure] Document<TMember> document)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var d = this.Read(document.Key);
            if (d.ETag != document.ETag)
            {
                throw new ETagMismatchException($"key: {document.Key}, expected: {d.ETag}, actual: {document.ETag}");
            }

            this.UpdateDocument(document);

            this.AfterUpdate?.Invoke(this, new DocumentUpdatedEventArgs<TMember>(document));
        }

        /// <inheritdoc/>
        public void Update([Pure] IEnumerable<Document<TMember>> documents)
        {
            if (documents is null)
            {
                throw new ArgumentNullException(nameof(documents));
            }

            foreach (var document in documents)
            {
                this.Update(document);
            }
        }

        /// <inheritdoc/>
        [Pure]
        public abstract IEnumerator<Document<TMember>> GetEnumerator();

        /// <inheritdoc/>
        [Pure]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        protected abstract void AddDocument([Pure] Document<TMember> document);

        protected abstract void ClearCollection();

        [Pure]
        protected abstract bool ContainsDocument(string key);

        [Pure]
        protected abstract Document<TMember> ReadDocument(string key);

        [Pure]
        protected abstract void RemoveDocument(string key);

        protected abstract void UpdateDocument([Pure] Document<TMember> document);

        [Pure]
        private IEnumerable<Document<TMember>> ReadDocuments(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                yield return this.Read(key);
            }
        }
    }
}
