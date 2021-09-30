using System;
using System.IO;

namespace Documents.IO.Tests
{
    internal sealed class DeleterFileTestsFixture
        : IDisposable
    {
        public static string Path { get; } = Guid.NewGuid().ToString();

        public DeleterFileTestsFixture()
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

        public static string MakeFilePath()
        {
            var path = System.IO.Path.Combine(MakePath(), Guid.NewGuid().ToString());

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

    internal sealed class ReaderFileTestsFixture
        : IDisposable
    {
        public static string Path { get; } = Guid.NewGuid().ToString();

        public ReaderFileTestsFixture()
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

        public static string MakeFilePath()
        {
            var path = System.IO.Path.Combine(MakePath(), Guid.NewGuid().ToString());

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

    internal sealed class WriterFileTestsFixture
        : IDisposable
    {
        public static string Path { get; } = Guid.NewGuid().ToString();

        public WriterFileTestsFixture()
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

        public static string MakeFilePath()
        {
            var path = System.IO.Path.Combine(MakePath(), Guid.NewGuid().ToString());

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
