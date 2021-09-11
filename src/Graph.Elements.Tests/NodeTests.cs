﻿using Graph.Elements;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Graph.Tests
{
    public sealed class NodeSerializationTests
    {
        [Fact]
        public void Node_Json_Serialize_Deserialize()
        {
            var node = Node.New;
            _ = Edge.Couple(node, Node.New);
            _ = Edge.Couple(node, Node.New);
            _ = Edge.Couple(node, Node.New);

            var json = JsonConvert.SerializeObject(node);
            Assert.False(String.IsNullOrWhiteSpace(json));
            var clone = JsonConvert.DeserializeObject<Node>(json);
            Assert.NotNull(clone);
            Assert.Equal(node, clone);
            Assert.True(node.Equals(clone));
        }
    }

    public sealed class NodeTests
    {
        [Fact]
        public void Node_New_Sets_Id()
        {
            var node = Node.New;
            Assert.NotNull(node);
            Assert.NotEqual(Guid.Empty, node.Id);
        }

        [Fact]
        public void Node_Clone_Copies_Values()
        {
            var node = Node.New;
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(clone));
            Assert.Equal(node.Id, clone.Id);
        }

        [Fact]
        public void Node_Equals_With_Null_Returns_False()
        {
            var node = Node.New;
            Assert.False(node.Equals(null));
        }

        [Fact]
        public void Node_Equals_X_Y_Returns_True()
        {
            var node = Node.New;
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(node, clone));
        }

        [Fact]
        public void Node_Equals_X_Y_With_Null_Returns_False()
        {
            var node = Node.New;
            Assert.False(node.Equals(node, null));
            Assert.False(node.Equals(null, node));
        }

        [Fact]
        public void Node_Equals_Obj_Returns_True()
        {
            var node = Node.New;
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(clone as object));
        }

        [Fact]
        public void Node_Equals_Obj_Returns_False()
        {
            var other = String.Empty;
            var node = Node.New;
            Assert.False(node.Equals(other));

            Assert.False(node.Equals(other));
        }

        [Fact]
        public void Node_Equals_Returns_False()
        {
            var node = Node.New;
            var other = Node.New;
            Assert.False(node.Equals(other));
        }

        [Fact]
        public void Node_GetHashCode_Obj_Equal_GetHashCode()
        {
            var node1 = Node.New;
            var node2 = Node.New;
            Assert.NotEqual(node1.GetHashCode(), node2.GetHashCode());
            Assert.Equal(node1.GetHashCode(), node2.GetHashCode(node1));
        }

        [Fact]
        public void Node_GetHashCode_Obj_Throws_ArgumentNullException()
        {
            var node = Node.New;
            Assert.Throws<ArgumentNullException>(() => node.GetHashCode(null));
        }

        [Fact]
        public void Node_Degree_Returns_Zero()
        {
            Assert.Equal(0, Node.New.Degree);
        }

        [Fact]
        public void Node_TryCouple_Throws_On_Null()
        {
            Assert.Throws<ArgumentNullException>(() => Node.New.Couple(null));
        }

        [Fact]
        public void Node_IsAdjacent_Throws_On_Null()
        {
            var node = Node.New;
            Assert.Throws<ArgumentNullException>(() => _ = node.IsAdjacent(null));
        }

        [Fact]
        public void Node_Degree_Increases_With_Couple()
        {
            var node = Node.New;
            for (var i = 0; i < 3; ++i)
            {
                Assert.Equal(i, node.Degree);
                Assert.NotNull(Edge.Couple(node, Node.New));
            }
        }

        [Fact]
        public void Node_Adjacent_Returns_True()
        {
            var node = Node.New;
            for (var i = 0; i < 3; ++i)
            {
                var other = Node.New;
                Assert.NotNull(Edge.Couple(node, other));
                Assert.True(node.IsAdjacent(other));
                Assert.True(node.IsAdjacent(other.Id));
            }
        }

        [Fact]
        public void Node_Adjacent_Returns_False()
        {
            var node = Node.New;
            for (var i = 0; i < 3; ++i)
            {
                var coupled = Node.New;
                Assert.NotNull(Edge.Couple(node, coupled));
                Assert.True(node.IsAdjacent(coupled));
                Assert.True(node.IsAdjacent(coupled.Id));
            }

            var other = Node.New;
            Assert.False(node.IsAdjacent(other));
            Assert.False(node.IsAdjacent(other.Id));
        }

        [Fact]
        public void Node_TryDecouple_Returns_True()
        {
            var node = Node.New;
            for (var i = 0; i < 3; ++i)
            {
                var other = Node.New;
                var edge = Edge.Couple(node, other);
                Assert.NotNull(edge);
                Assert.True(node.IsAdjacent(other));
                node.Decouple(edge);
                Assert.False(node.IsAdjacent(other));
            }
            Assert.Equal(0, node.Degree);
        }

        [Fact]
        public void Node_TryDecouple_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Node.New.Decouple(null));
        }

        [Fact]
        public void Node_Neighbors_Enumerates_Adjacent_Nodes()
        {
            var node = Node.New;
            var other = Node.New;
            _ = Edge.Couple(node, other);
            Assert.Contains(other.Id, node.Neighbors);
        }

        [Fact]
        public void Node_Addition_Operator()
        {
            var source = Node.New;
            var target = Node.New;
            var edge = source + target;
            Assert.NotNull(edge);
            Assert.True(edge.IsDirected);
            Assert.Equal(source.Id, edge.SourceId);
            Assert.Equal(target.Id, edge.TargetId);
            Assert.True(source.IsAdjacent(target));
        }
    }
}
