using System;
using System.IO;

namespace Documents.Collections.IO.Tests
{
    internal sealed class FileTestsFixture
        : IDisposable
    {
        public static string Path { get; } = Guid.NewGuid().ToString();

        public FileTestsFixture()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public static string MakePath()
        {
            var path = System.IO.Path.Combine(Path, Guid.NewGuid().ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
        }
    }
}
