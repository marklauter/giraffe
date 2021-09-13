using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class AsyncFileReader
        : AsyncSafeFileAccessor
        , IAsyncFileReader
    {
        private readonly System.Text.Encoding encoding;

        public AsyncFileReader(TimeSpan timeout)
            : this(timeout, System.Text.Encoding.UTF8)
        {
        }

        public AsyncFileReader(TimeSpan timeout, System.Text.Encoding encoding)
            : base(timeout)
        {
            this.encoding = encoding;
        }

        public async Task<string> ReadAsync(string path)
        {
            var wait = new SpinWait();
            var start = DateTime.UtcNow;
            var lastIoEx = default(IOException);
            do
            {
                try
                {
                    using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                    using var reader = new StreamReader(stream, this.encoding);
                    return await reader.ReadToEndAsync();
                }
                catch (IOException ex)
                {
                    if (!IsFileLocked(ex))
                    {
                        throw;
                    }

                    lastIoEx = ex;
                    wait.SpinOnce();
                }
            }
            while (DateTime.UtcNow - start < this.Timeout);

            throw new TimeoutException(nameof(ReadAsync), lastIoEx);
        }
    }
}
