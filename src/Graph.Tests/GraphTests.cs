using System;
using Xunit;

namespace Graphs.Tests
{
    public class GraphTests
    {
        [Fact]
        public void Graph_Constructor_Throws_On_Nulls()
        {
            Assert.Throws<ArgumentNullException>(() => new Graph());
        }
    }
}
