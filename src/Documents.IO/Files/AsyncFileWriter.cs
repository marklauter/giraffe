using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class AsyncFileWriter
        : AsyncSafeFileAccessor
        , IAsyncFileWriter
    {
        private readonly System.Text.Encoding encoding;

        public AsyncFileWriter(TimeSpan timeout)
            : this(timeout, System.Text.Encoding.UTF8)
        {
        }

        public AsyncFileWriter(TimeSpan timeout, System.Text.Encoding encoding)
            : base(timeout)
        {
            this.encoding = encoding;
        }

        public async Task WriteAsync(string path, string text)
        {
            await Task.Run(async () =>
            {
                var wait = new SpinWait();
                var start = DateTime.UtcNow;
                var lastIoEx = default(IOException);
                do
                {
                    try
                    {
                        using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                        using var writer = new StreamWriter(stream, this.encoding);
                        await writer.WriteAsync(text);
                        return;
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
            });
        }
    }
}
