using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class ThreadSafeFileReader
        : ThreadSafeFileAccessor
        , IAsyncFileReader
    {
        private readonly System.Text.Encoding encoding;

        public ThreadSafeFileReader(TimeSpan timeout)
            : this(timeout, Encoding.UTF8)
        {
        }

        public ThreadSafeFileReader(TimeSpan timeout, System.Text.Encoding encoding)
            : base(timeout)
        {
            this.encoding = encoding;
        }

        public Task<string> ReadAsync(string path)
        {
            var wait = new SpinWait();
            var start = DateTime.UtcNow;
            IOException lastIoEx;
            do
            {
                try
                {
                    using var reader = new StreamReader(path, this.encoding);
                    return reader.ReadToEndAsync();
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
