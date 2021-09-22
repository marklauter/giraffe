using Documents.IO.Files;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Documents.IO.Tests
{
    public class AsyncFileWriterTests
        : IClassFixture<WriterFileTestsFixture>
    {
        [Fact]
        public void AsyncFileWriter_Constructor_Throws_On_Zero_Timeout()
        {
            Assert.Throws<ArgumentException>(() => new AsyncFileWriter(TimeSpan.MinValue));
        }

        [Fact]
        public void AsyncFileWriter_Constructor_Throws_On_Null_Encoding()
        {
            Assert.Throws<ArgumentNullException>(() => new AsyncFileWriter(TimeSpan.FromSeconds(10), null));
        }

        [Fact]
        public async Task AsyncFileWriter_Constructor_WriteAsync_Throws_On_Open_File_Timeout_Async()
        {
            var path = WriterFileTestsFixture.MakeFilePath();
            using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));

            var ex = await Assert.ThrowsAsync<TimeoutException>(() => writer.WriteAsync(path, "x"));
            Assert.NotNull(ex.InnerException);
            Assert.Equal(typeof(IOException), ex.InnerException.GetType());
        }

        [Fact]
        public async Task AsyncFileWriter_Constructor_WriteAsync_Throws_On_Invalid_FilePath_Async()
        {
            var path = WriterFileTestsFixture.MakePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => writer.WriteAsync(path, "x"));
        }

        [Fact]
        public async Task AsyncFileWriter_Constructor_WriteAsync_Succeeds_Async()
        {
            var path = WriterFileTestsFixture.MakeFilePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            Assert.False(File.Exists(path));
            await writer.WriteAsync(path, "x");
            Assert.True(File.Exists(path));
        }
    }
}
