using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Documents.IO.Files
{
    public sealed class ThreadSafeFileDeleter
        : ThreadSafeFileAccessor
    {
        public ThreadSafeFileDeleter(TimeSpan timeout)
            : base(timeout)
        {
        }

        public Task DeleteAsync(string path)
        {
            var wait = new SpinWait();
            var start = DateTime.UtcNow;
            IOException lastIoEx;
            do
            {
                try
                {
                    return Task.Run(() => File.Delete(path));
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
        }
    }
}
