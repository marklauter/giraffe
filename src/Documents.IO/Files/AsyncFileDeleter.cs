using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class AsyncFileDeleter
        : AsyncSafeFileAccessor
        , IAsyncFileDeleter
    {
        public AsyncFileDeleter(TimeSpan timeout)
            : base(timeout)
        {
        }

        public async Task DeleteAsync(string path)
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
                        await Task.Run(() => File.Delete(path));
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

                throw new TimeoutException(nameof(DeleteAsync), lastIoEx);
            });
        }
    }
}
