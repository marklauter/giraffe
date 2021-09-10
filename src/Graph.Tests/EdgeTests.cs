using Graph.Elements;
using System;
using Xunit;

namespace Graph.Tests
{
    public sealed class EdgeTests
    {
        [Fact]
        public void Edge_New_Sets_Source_Target_and_Directed_False()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.NotNull(edge);
            Assert.NotEqual(Guid.Empty, edge.Id);
            Assert.Equal(source.Id, edge.SourceId);
            Assert.Equal(target.Id, edge.TargetId);
            Assert.False(edge.IsDirected);
        }

        [Fact]
        public void Edge_New_Sets_Source_Target_and_Directed_True()
        {
            var directed = true;
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target, directed);
            Assert.NotEqual(Guid.Empty, edge.Id);
            Assert.NotNull(edge);
            Assert.Equal(source.Id, edge.SourceId);
            Assert.Equal(target.Id, edge.TargetId);
            Assert.Equal(directed, edge.IsDirected);
        }

        [Fact]
        public void Edge_Clone_Copies_Values()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            var clone = edge.Clone() as Edge;
            Assert.True(edge.Equals(clone));
            Assert.Equal(edge.SourceId, clone.SourceId);
            Assert.Equal(edge.TargetId, clone.TargetId);
            Assert.Equal(edge.IsDirected, clone.IsDirected);
        }

        [Fact]
        public void Edge_Equals_With_Null_Returns_False()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.False(edge.Equals(null));
        }

        [Fact]
        public void Edge_Equals_X_Y_Returns_True()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            var clone = edge.Clone() as Edge;
            Assert.True(edge.Equals(edge, clone));
        }

        [Fact]
        public void Edge_Equals_X_Y_With_Null_Returns_False()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.False(edge.Equals(edge, null));
            Assert.False(edge.Equals(null, edge));
        }

        [Fact]
        public void Edge_Equals_Obj_Returns_True()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            var clone = edge.Clone() as Edge;
            Assert.True(edge.Equals(clone as object));
        }

        [Fact]
        public void Edge_Equals_Obj_Returns_False()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            var other = Edge.New(source, target);
            Assert.False(edge.Equals(other as object));

            var node = Node.New;
            Assert.False(edge.Equals(node));
        }

        [Fact]
        public void Edge_Equals_Returns_False()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            var other = Edge.New(source, target);
            Assert.False(edge.Equals(other));
        }

        [Fact]
        public void Edge_GetHashCode_Obj_Equal_GetHashCode()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.Equal(edge.GetHashCode(edge), edge.GetHashCode());
        }

        [Fact]
        public void Edge_GetHashCode_Obj_Throws_ArgumentNullException()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.Throws<ArgumentNullException>(() => edge.GetHashCode(null));
        }

        [Fact]
        public void Edge_Enumerates_Values()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = Edge.New(source, target);
            Assert.Contains(source.Id, edge.Nodes());
            Assert.Contains(target.Id, edge.Nodes());
        }
    }
}
