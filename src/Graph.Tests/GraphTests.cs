using Documents.Collections;
using Graphs.Data;
using Graphs.Elements;
using System;
using Xunit;

namespace Graphs.Tests
{
    public class GraphTests
    {
        [Fact]
        public void Graph_Constructor_Throws_On_Nulls()
        {
            Assert.Throws<ArgumentNullException>(() => new Graph(null));
        }

        [Fact]
        public void GraphDocumentContext_Constructor_Throws_On_Nulls()
        {
            Assert.Throws<ArgumentNullException>(() => new GraphDocumentContext(HeapDocumentCollection<Node>.Empty, null));
            Assert.Throws<ArgumentNullException>(() => new GraphDocumentContext(null, HeapDocumentCollection<Edge>.Empty));
        }

        [Fact]
        public void Graph_New_Is_Empty()
        {
            var context = new GraphDocumentContext(HeapDocumentCollection<Node>.Empty, HeapDocumentCollection<Edge>.Empty);
            var graph = new Graph(context);
            Assert.True(graph.IsEmpty);
        }
    }
}


