using Documents.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Documents.IO
{
    public sealed class FileSystemDocumentCollection<TMember>
        : DocumentCollection<TMember>
        , IDisposable
        where TMember : class
    {
        private static readonly string TypeName = typeof(TMember).Name;

        private readonly string path;
        private readonly TimeSpan fileLockTimeout;
        private readonly IDocumentSerializer<TMember> serializer;
        private readonly DocumentActionQueueProcessor<TMember> actionQueue;
        private bool disposedValue;

        public FileSystemDocumentCollection(
            string path,
            TimeSpan fileLockTimeout)
            : this(path, fileLockTimeout, null)
        {
        }

        public FileSystemDocumentCollection(
            string path,
            TimeSpan fileLockTimeout,
            IDocumentSerializer<TMember> serializer)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));
            }

            this.path = path;
            this.fileLockTimeout = fileLockTimeout;
            this.serializer = serializer ?? new JsonDocumentSerializer<TMember>();
            this.actionQueue = new DocumentActionQueueProcessor<TMember>(
                this.DeleteFile,
                this.WriteFile);
        }

        public override int Count => Directory.EnumerateFiles(this.path).Count();

        public override IEnumerator<Document<TMember>> GetEnumerator()
        {
            foreach (var file in Directory.EnumerateFiles(this.path))
            {
                yield return this.ReadFile(file);
            }
        }

        protected override void AddDocument(Document<TMember> document)
        {
            this.actionQueue.EnqueueAddAction(document);
        }

        protected override void ClearCollection()
        {
            foreach (var file in Directory.EnumerateFiles(this.path))
            {
                this.DeleteFile(file);
            }
        }

        protected override bool ContainsDocument(string key)
        {
            return String.IsNullOrWhiteSpace(key)
                ? throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key))
                : File.Exists(this.GetFileName(key));
        }

        protected override Document<TMember> ReadDocument(string key)
        {
            return this.ReadDocumentWithKey(key);
        }

        protected override void RemoveDocument(string key)
        {
            this.actionQueue.EnqueueRemoveAction(key);
        }

        protected override void UpdateDocument(Document<TMember> document)
        {
            this.actionQueue.EnqueueUpdateAction(document);
        }

        private void DeleteFile(string key)
        {
            ThreadSafeFile.Delete(this.GetFileName(key), this.fileLockTimeout);
        }

        private Document<TMember> ReadDocumentWithKey(string key)
        {
            return this.ReadFile(this.GetFileName(key));
        }

        private Document<TMember> ReadFile(string fileName)
        {
            using var stream = ThreadSafeFile.Open(
                fileName,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                this.fileLockTimeout);

            return this.serializer.Deserialize(stream);
        }

        private void WriteFile(Document<TMember> document)
        {
            using var stream = ThreadSafeFile.Open(
                this.GetFileName(document.Key),
                FileMode.OpenOrCreate,
                FileAccess.Write,
                FileShare.None,
                this.fileLockTimeout);

            this.serializer.Serialize(document, stream);
            stream.Flush(true);
        }

        private string GetFileName(string key)
        {
            return Path.Combine(this.path, $"{key}.{TypeName}");
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.actionQueue.Dispose();
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
