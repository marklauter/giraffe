using System;
using Xunit;

namespace Documents.Tests
{
    public sealed class ArgumentTests
    {
        [Fact]
        public void Fail()
        {
            throw new Exception("write some tests!");
        }
    }
}
