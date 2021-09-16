using Documents.IO.Files;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Documents.IO.Tests
{
    public class AsyncFileDeleterTests
        : IClassFixture<DeleterFileTestsFixture>
    {
        [Fact]
        public void AsyncFileDeleter_Constructor_Throws_On_Zero_Timeout()
        {
            Assert.Throws<ArgumentException>(() => new AsyncFileDeleter(TimeSpan.MinValue));
        }

        [Fact]
        public async Task AsyncFileDeleter_Constructor_DeleteAsync_Throws_On_Open_File_Timeout_Async()
        {
            var path = DeleterFileTestsFixture.MakeFilePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            await writer.WriteAsync(path, "x");
            Assert.True(File.Exists(path));

            var deleter = new AsyncFileDeleter(TimeSpan.FromMilliseconds(1));

            using var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);

            var ex = await Assert.ThrowsAsync<TimeoutException>(() => deleter.DeleteAsync(path));
            Assert.NotNull(ex.InnerException);
            Assert.Equal(typeof(IOException), ex.InnerException.GetType());
        }

        [Fact]
        public async Task AsyncFileDeleter_Constructor_DeleteAsync_Throws_On_Invalid_FilePath_Async()
        {
            var path = DeleterFileTestsFixture.MakePath();
            var deleter = new AsyncFileDeleter(TimeSpan.FromMilliseconds(1));
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => deleter.DeleteAsync(path));
        }

        [Fact]
        public async Task AsyncFileDeleter_ConstructorDeleteAsync_Succeeds_Async()
        {
            var path = DeleterFileTestsFixture.MakeFilePath();
            var writer = new AsyncFileWriter(TimeSpan.FromMilliseconds(1));
            Assert.False(File.Exists(path));
            await writer.WriteAsync(path, "x");
            Assert.True(File.Exists(path));

            var deleter = new AsyncFileDeleter(TimeSpan.FromMilliseconds(1));
            await deleter.DeleteAsync(path);
            Assert.False(File.Exists(path));
        }
    }
}
