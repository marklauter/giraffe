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
            Assert.Throws<ArgumentNullException>(() => new Graph<Guid>(null));
        }

        [Fact]
        public void GraphDocumentContext_Constructor_Throws_On_Nulls()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentContext<Guid>(HeapDocumentCollection<Node<Guid>>.Empty, null));
            Assert.Throws<ArgumentNullException>(() => new DocumentContext<Guid>(null, HeapDocumentCollection<Edge<Guid>>.Empty));
        }

        [Fact]
        public void GraphDocumentContext_Constructor_Sets_Collections()
        {
            var nodes = HeapDocumentCollection<Node<Guid>>.Empty;
            var edges = HeapDocumentCollection<Edge<Guid>>.Empty;
            var context = new DocumentContext<Guid>(nodes, edges);
            Assert.Equal(nodes, context.Nodes);
            Assert.Equal(edges, context.Edges);
        }

        [Fact]
        public void Graph_New_Is_Empty()
        {
            var context = new DocumentContext<Guid>(
                HeapDocumentCollection<Node<Guid>>.Empty,
                HeapDocumentCollection<Edge<Guid>>.Empty);

            var graph = new Graph<Guid>(context);
            Assert.True(graph.IsEmpty);
        }
    }
}


