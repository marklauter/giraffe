﻿using Graph.Elements;
using System;
using Xunit;

namespace Graph.Tests
{
    public sealed class AdjacencyAndIncidenceIndexTests
    {
        [Fact]
        public void Index_Empty_Returns_Empty()
        {
            var index = AdjacencyAndIncidenceIndex.Empty;
            Assert.True(index.IsEmpty);
        }

        [Fact]
        public void Index_Count_Returns_Expected_Value()
        {
            var source1 = Node.New;
            var target1 = Node.New;
            var edge1 = new Edge(Guid.NewGuid(), source1.Id, target1.Id, true);

            var source2 = Node.New;
            var target2 = Node.New;
            var edge2 = new Edge(Guid.NewGuid(), source2.Id, target2.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty;
            Assert.Equal(0, index.EdgeCount);
            Assert.Equal(0, index.NodeCount);

            _ = index.Add(edge1.Id, target1.Id);
            Assert.Equal(1, index.EdgeCount);
            Assert.Equal(1, index.NodeCount);

            _ = index.Add(edge2.Id, target2.Id);
            Assert.Equal(2, index.EdgeCount);
            Assert.Equal(2, index.NodeCount);

            _ = index.Remove(edge1.Id, target1.Id);
            Assert.Equal(1, index.EdgeCount);
            Assert.Equal(1, index.NodeCount);
        }

        [Fact]
        public void Index_Add_Updates_Edges_Nodes_and_NodesToEdges()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = new Edge(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty;
            index.Add(edge.Id, target.Id);

            Assert.True(index.ContainsNode(target.Id));
            Assert.False(index.ContainsNode(source.Id));

            Assert.True(index.ContainsEdge(edge.Id));
            Assert.False(index.ContainsEdge(target.Id));
            Assert.False(index.ContainsEdge(source.Id));
        }

        [Fact]
        public void Index_Add_Ignores_Duplicates()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = new Edge(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty;
            index.Add(edge.Id, target.Id);
            index.Add(edge.Id, target.Id);

            Assert.True(index.ContainsNode(target.Id));
            Assert.True(index.ContainsEdge(edge.Id));

            Assert.Equal(1, index.EdgeCount);
            Assert.Equal(1, index.NodeCount);

            Assert.Single(index.Nodes);
            Assert.Single(index.Edges);
        }

        [Fact]
        public void Index_Add_Recursive_Edge_Updates_Node_Reference_Count()
        {
            var source = Node.New;
            var target = Node.New;
            var edge1 = new Edge(Guid.NewGuid(), source.Id, target.Id, true);
            var edge2 = new Edge(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty
                .Add(edge1.Id, target.Id)
                .Add(edge2.Id, target.Id);

            Assert.True(index.ContainsNode(target.Id));
            Assert.True(index.ContainsEdge(edge1.Id));
            Assert.True(index.ContainsEdge(edge2.Id));

            Assert.Equal(2, index.EdgeCount);
            Assert.Equal(1, index.NodeCount);

            Assert.Equal(2, index.NodeReferenceCount(target.Id));
        }

        [Fact]
        public void Index_Remove_Recursive_Edge_Updates_Node_Reference_Count()
        {
            var source = Node.New;
            var target = Node.New;
            var edge1 = new Edge(Guid.NewGuid(), source.Id, target.Id, true);
            var edge2 = new Edge(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty
                .Add(edge1.Id, target.Id)
                .Add(edge2.Id, target.Id);

            Assert.Equal(2, index.EdgeCount);
            Assert.Equal(1, index.NodeCount);

            Assert.Equal(2, index.NodeReferenceCount(target.Id));

            _ = index.Remove(edge2.Id, target.Id);

            Assert.Equal(1, index.NodeReferenceCount(target.Id));
            Assert.True(index.ContainsNode(target.Id));
            Assert.True(index.ContainsEdge(edge1.Id));
            Assert.False(index.ContainsEdge(edge2.Id));
        }

        [Fact]
        public void Index_Clone_Copies_Edges_Nodes_and_NodesToEdges()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = new Edge(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyAndIncidenceIndex.Empty;
            index.Add(edge.Id, target.Id);
            index.Add(edge.Id, target.Id);

            Assert.True(index.ContainsNode(target.Id));
            Assert.True(index.ContainsEdge(edge.Id));

            var clone = index.Clone() as AdjacencyAndIncidenceIndex;
            Assert.True(clone.ContainsNode(target.Id));
            Assert.True(clone.ContainsEdge(edge.Id));
        }
    }
}