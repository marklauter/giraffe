using Documents.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Documents.IO
{
    public sealed class FileDocumentCollection<TMember>
        : DocumentCollection<TMember>
        where TMember : class
    {
        private static readonly string TypeName = typeof(TMember).Name;

        private readonly string path;
        private readonly TimeSpan fileLockTimeout;
        private readonly IDocumentSerializer<TMember> serializer;

        public FileDocumentCollection(
            string path,
            TimeSpan fileLockTimeout)
            : this(path, fileLockTimeout, null)
        {
        }

        public FileDocumentCollection(
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
            this.WriteFile(document);
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
            return File.Exists(this.GetFileName(key));
        }

        protected override Document<TMember> ReadDocument(string key)
        {
            return this.ReadDocumentWithKey(key);
        }

        protected override void RemoveDocument(string key)
        {
            this.DeleteFile(this.GetFileName(key));
        }

        protected override void UpdateDocument(Document<TMember> document)
        {
            this.WriteFile(document);
        }

        private void DeleteFile(string fileName)
        {
            ThreadSafeFile.Delete(fileName, this.fileLockTimeout);
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
        }

        private string GetFileName(string key)
        {
            return Path.Combine(this.path, $"{key}.{TypeName}");
        }
    }
}
