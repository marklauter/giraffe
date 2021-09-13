using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Documents.IO.Files
{
    public abstract class ThreadSafeFileAccessor
    {
        protected readonly TimeSpan Timeout;

        private const uint HRFileLocked = 0x80070020;
        private const uint HRPortionOfFileLocked = 0x80070021;

        protected ThreadSafeFileAccessor(TimeSpan timeout)
        {
            this.Timeout = timeout;
        }

        protected static bool IsFileLocked(IOException ex)
        {
            var errorCode = (uint)Marshal.GetHRForException(ex);
            return errorCode == HRFileLocked || errorCode == HRPortionOfFileLocked;
        }
    }
}
