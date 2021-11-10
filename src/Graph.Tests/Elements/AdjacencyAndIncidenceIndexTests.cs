using Graphs.Identifiers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graphs.Elements.Tests
{
    public sealed class AdjacencyAndIncidenceIndexTests
    {
        private static readonly IIdGenerator<Guid> IdGenerator = new DefaultIdGenerator();

        [Fact]
        public void Index_Empty_Returns_Empty()
        {
            var index = AdjacencyIndex<Guid>.Empty;
            Assert.True(index.IsEmpty);
        }

        [Fact]
        public void Index_Count_Returns_Expected_Value()
        {
            var source1 = Node<Guid>.New(IdGenerator);
            var target1 = Node<Guid>.New(IdGenerator);
            var edge1 = new Edge<Guid>(Guid.NewGuid(), source1.Id, target1.Id, true);

            var source2 = Node<Guid>.New(IdGenerator);
            var target2 = Node<Guid>.New(IdGenerator);
            var edge2 = new Edge<Guid>(Guid.NewGuid(), source2.Id, target2.Id, true);

            var index = AdjacencyIndex<Guid>.Empty;
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
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty;
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
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty;
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
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge1 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);
            var edge2 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty
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
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge1 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);
            var edge2 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty
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
        public void Index_NodeReferenceCount_Returns_Zero()
        {
            Assert.Equal(0, AdjacencyIndex<Guid>.Empty
                .ReferenceCount(Guid.Empty));
        }

        [Fact]
        public void Index_Remove_Throws_When_Edge_Not_Found()
        {
            Assert.Throws<KeyNotFoundException>(() =>
                AdjacencyIndex<Guid>.Empty.Remove(Guid.Empty, Guid.Empty));
        }

        [Fact]
        public void Index_Clone_Copies_Edges_Nodes_and_NodesToEdges()
        {
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty;
            index.Add(edge.Id, target.Id);
            index.Add(edge.Id, target.Id);

            Assert.True(index.ContainsNode(target.Id));
            Assert.True(index.ContainsEdge(edge.Id));

            var clone = index.Clone() as AdjacencyIndex<Guid>;
            Assert.True(clone.ContainsNode(target.Id));
            Assert.True(clone.ContainsEdge(edge.Id));
        }

        [Fact]
        public void Index_Remove_All_Edges_Removes_Node()
        {
            var source = Node<Guid>.New(IdGenerator);
            var target = Node<Guid>.New(IdGenerator);
            var edge1 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);
            var edge2 = new Edge<Guid>(Guid.NewGuid(), source.Id, target.Id, true);

            var index = AdjacencyIndex<Guid>.Empty
                .Add(edge1.Id, target.Id)
                .Add(edge2.Id, target.Id);

            Assert.Equal(2, index.NodeReferenceCount(target.Id));

            _ = index.Remove(edge1.Id, target.Id);
            _ = index.Remove(edge2.Id, target.Id);

            Assert.Equal(0, index.NodeReferenceCount(target.Id));
            Assert.False(index.ContainsNode(target.Id));
        }
    }
}
