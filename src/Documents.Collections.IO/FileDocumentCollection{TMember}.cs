using Documents.IO.Encoding;
using Documents.IO.Files;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Documents.Collections.IO
{
    // todo: TPL would probably be useful for writes to the file system https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library
    public sealed class FileDocumentCollection<TMember>
        : DocumentCollection<TMember>
        where TMember : class
    {
        private static readonly string TypeName = typeof(TMember).Name;

        private readonly string path;
        private readonly IDocumentEncoder<TMember> encoder;
        private readonly IDocumentDecoder<TMember> decoder;
        private readonly IAsyncFileReader reader;
        private readonly IAsyncFileWriter writer;
        private readonly IAsyncFileDeleter deleter;

        public FileDocumentCollection(
            string path,
            IDocumentEncoder<TMember> encoder,
            IDocumentDecoder<TMember> decoder,
            IAsyncFileReader reader,
            IAsyncFileWriter writer,
            IAsyncFileDeleter deleter)
        {
            if (String.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));
            }

            this.path = path;
            this.encoder = encoder;
            this.decoder = decoder;
            this.reader = reader;
            this.writer = writer;
            this.deleter = deleter;
        }

        public override int Count => Directory.EnumerateFiles(this.path).Count();

        protected override Task ClearCollectionAsync()
        {
            var tasks = Directory.EnumerateFiles(this.path)
                .Select(file => this.DeleteFileAsync(file));

            return Task.WhenAll(tasks);
        }

        protected override Task<bool> ContainsDocumentAsync(string key)
        {
            return Task.Run(() => File.Exists(this.GetFileName(key)));
        }

        protected override Task<Document<TMember>> ReadDocumentAsync(string key)
        {
            return this.ReadDocumentWithKeyAsync(key);
        }

        protected override Task RemoveDocumentAsync(string key)
        {
            return this.DeleteFileAsync(this.GetFileName(key));
        }

        protected override Task WriteDocumentAsync([Pure] Document<TMember> document)
        {
            return this.WriteFileAsync(document);
        }

        private Task DeleteFileAsync(string path)
        {
            return this.deleter.DeleteAsync(path);
        }

        private Task<Document<TMember>> ReadDocumentWithKeyAsync(string key)
        {
            return this.ReadFileAsync(this.GetFileName(key));
        }

        private async Task<Document<TMember>> ReadFileAsync(string path)
        {
            var json = await this.reader.ReadAsync(path);
            return await this.decoder.DeserializeAsync(json);
        }

        private async Task WriteFileAsync([Pure] Document<TMember> document)
        {
            var json = await this.encoder.SerializeAsync(document);
            await this.writer.WriteAsync(this.GetFileName(document.Key), json);
        }

        private string GetFileName(string key)
        {
            return Path.Combine(this.path, $"{key}.{TypeName}");
        }
    }
}
