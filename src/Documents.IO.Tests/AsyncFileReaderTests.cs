using Documents.IO.Files;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Documents.IO.Tests
{
    public class AsyncFileReaderTests
        : IClassFixture<ReaderFileTestsFixture>
    {
        [Fact]
        public void AsyncFileReader_Constructor_Throws_On_Zero_Timeout()
        {
            Assert.Throws<ArgumentException>(() => new AsyncFileReader(TimeSpan.MinValue));
        }

        [Fact]
        public void AsyncFileReader_Constructor_Throws_On_Null_Encoding()
        {
            Assert.Throws<ArgumentNullException>(() => new AsyncFileReader(TimeSpan.FromSeconds(10), null));
        }

        [Fact]
        public async Task AsyncFileReader_Constructor_ReadAsync_Throws_On_Open_File_Timeout_Async()
        {
            var path = ReaderFileTestsFixture.MakeFilePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            await writer.WriteAsync(path, "x");
            Assert.True(File.Exists(path));

            var reader = new AsyncFileReader(TimeSpan.FromMilliseconds(1));

            using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            var ex = await Assert.ThrowsAsync<TimeoutException>(() => reader.ReadAsync(path));
            Assert.NotNull(ex.InnerException);
            Assert.Equal(typeof(IOException), ex.InnerException.GetType());
        }

        [Fact]
        public async Task AsyncFileReader_Constructor_ReadAsync_Throws_On_Invalid_FilePath_Async()
        {
            var path = ReaderFileTestsFixture.MakePath();
            var reader = new AsyncFileReader(TimeSpan.FromMilliseconds(1));
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => reader.ReadAsync(path));
        }

        [Fact]
        public async Task AsyncFileReader_Constructor_ReadAsync_Succeeds_Async()
        {
            var path = ReaderFileTestsFixture.MakeFilePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            Assert.False(File.Exists(path));
            await writer.WriteAsync(path, "x");
            Assert.True(File.Exists(path));

            var reader = new AsyncFileReader(TimeSpan.FromMilliseconds(1));
            var x = await reader.ReadAsync(path);
            Assert.False(String.IsNullOrWhiteSpace(x));
            Assert.Equal("x", x);
        }
    }
}
