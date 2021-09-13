using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class ThreadSafeFileWriter
        : ThreadSafeFileAccessor
        , IAsyncFileWriter
    {
        private readonly System.Text.Encoding encoding;

        public ThreadSafeFileWriter(TimeSpan timeout)
            : this(timeout, Encoding.UTF8)
        {
        }

        public ThreadSafeFileWriter(TimeSpan timeout, System.Text.Encoding encoding)
            : base(timeout)
        {
            this.encoding = encoding;
        }

        public Task WriteAsync(string path, string text)
        {
            var wait = new SpinWait();
            var start = DateTime.UtcNow;
            IOException lastIoEx;
            do
            {
                try
                {
                    using var writer = new StreamWriter(path, false, this.encoding);
                    return writer.WriteAsync(text);
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

            throw new TimeoutException(nameof(WriteAsync), lastIoEx);
        }
    }
}
