using Newtonsoft.Json;
using System;
using Xunit;

namespace Graphs.Elements.Tests
{
    public sealed class NodeSerializationTests
    {
        [Fact]
        public void Node_Json_Serialize_Deserialize()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var edge1 = node + Node<Guid>.New(new GuidGenerator());
            var edge2 = node + Node<Guid>.New(new GuidGenerator());
            var edge3 = node + Node<Guid>.New(new GuidGenerator());

            var json = JsonConvert.SerializeObject(node);
            Assert.False(String.IsNullOrWhiteSpace(json));

            var clone = JsonConvert.DeserializeObject<Node>(json);
            Assert.NotNull(clone);
            Assert.Equal(node, clone);
            Assert.True(node.Equals(clone));
            Assert.True(clone.IsIncident(edge1));
            Assert.True(clone.IsIncident(edge2));
            Assert.True(clone.IsIncident(edge3));
            Assert.True(clone.IsAdjacent(edge1.TargetId));
            Assert.True(clone.IsAdjacent(edge2.TargetId));
            Assert.True(clone.IsAdjacent(edge3.TargetId));
        }
    }

    public sealed class NodeTests
    {
        [Fact]
        public void Node_New_Sets_Id()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            Assert.NotNull(node);
            Assert.NotEqual(Guid.Empty, node.Id);
        }

        [Fact]
        public void Node_Clone_Copies_Values()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(clone));
            Assert.Equal(node.Id, clone.Id);
        }

        [Fact]
        public void Node_Equals_With_Null_Returns_False()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            Assert.False(node.Equals(null));
        }

        [Fact]
        public void Node_Equals_X_Y_Returns_True()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(node, clone));
        }

        [Fact]
        public void Node_Equals_X_Y_With_Null_Returns_False()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            Assert.False(node.Equals(node, null));
            Assert.False(node.Equals(null, node));
        }

        [Fact]
        public void Node_Equals_Obj_Returns_True()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var clone = node.Clone() as Node;
            Assert.True(node.Equals(clone as object));
        }

        [Fact]
        public void Node_Equals_Obj_Returns_False()
        {
            var other = String.Empty;
            var node = Node<Guid>.New(new GuidGenerator());
            Assert.False(node.Equals(other));

            Assert.False(node.Equals(other));
        }

        [Fact]
        public void Node_Equals_Returns_False()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var other = Node<Guid>.New(new GuidGenerator());
            Assert.False(node.Equals(other));
        }

        [Fact]
        public void Node_GetHashCode_Obj_Equal_GetHashCode()
        {
            var node1 = Node<Guid>.New(new GuidGenerator());
            var node2 = Node<Guid>.New(new GuidGenerator());
            Assert.NotEqual(node1.GetHashCode(), node2.GetHashCode());
            Assert.Equal(node1.GetHashCode(), node2.GetHashCode(node1));
        }

        [Fact]
        public void Node_GetHashCode_Obj_Throws_ArgumentNullException()
        {
            var node = Node<Guid>.New(new GuidGenerator());
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
            var node = Node<Guid>.New(new GuidGenerator());
            Assert.Throws<ArgumentNullException>(() => _ = node.IsAdjacent(null));
        }

        [Fact]
        public void Node_Degree_Increases_With_Couple()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            for (var i = 0; i < 3; ++i)
            {
                Assert.Equal(i, node.Degree);
                Assert.NotNull(Edge<Guid>.Couple(node, Node.New));
            }
        }

        [Fact]
        public void Node_IsAdjacent_Returns_True()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            for (var i = 0; i < 3; ++i)
            {
                var other = Node<Guid>.New(new GuidGenerator());
                Assert.NotNull(Edge<Guid>.Couple(node, other));
                Assert.True(node.IsAdjacent(other));
                Assert.True(node.IsAdjacent(other.Id));
            }
        }

        [Fact]
        public void Node_IsAdjacent_Returns_False()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            for (var i = 0; i < 3; ++i)
            {
                var coupled = Node<Guid>.New(new GuidGenerator());
                Assert.NotNull(Edge<Guid>.Couple(node, coupled));
                Assert.True(node.IsAdjacent(coupled));
                Assert.True(node.IsAdjacent(coupled.Id));
            }

            var other = Node<Guid>.New(new GuidGenerator());
            Assert.False(node.IsAdjacent(other));
            Assert.False(node.IsAdjacent(other.Id));
        }

        [Fact]
        public void Node_IsIncident_Returns_True()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var other = Node<Guid>.New(new GuidGenerator());
            var edge = node + other;
            Assert.NotEmpty(node.IncidentEdges);
            Assert.True(node.IsIncident(edge));
        }

        [Fact]
        public void Node_IsIncident_Returns_False()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var other = Node<Guid>.New(new GuidGenerator());
            var edge = node + other;
            Assert.True(node.IsIncident(edge));

            node.Decouple(edge);
            Assert.False(node.IsIncident(edge));
            Assert.Empty(node.IncidentEdges);
        }

        [Fact]
        public void Node_IsIncident_Throws_When_Edge_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => Node.New.IsIncident(null));
        }

        [Fact]
        public void Node_TryDecouple_Returns_True()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            for (var i = 0; i < 3; ++i)
            {
                var other = Node<Guid>.New(new GuidGenerator());
                var edge = Edge<Guid>.Couple(node, other);
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
            var node = Node<Guid>.New(new GuidGenerator());
            var other = Node<Guid>.New(new GuidGenerator());
            _ = Edge<Guid>.Couple(node, other);
            Assert.Contains(other.Id, node.Neighbors);
        }

        [Fact]
        public void Node_Addition_Operator()
        {
            var source = Node<Guid>.New(new GuidGenerator());
            var target = Node<Guid>.New(new GuidGenerator());
            var edge = source + target;
            Assert.NotNull(edge);
            Assert.True(edge.IsDirected);
            Assert.Equal(source.Id, edge.SourceId);
            Assert.Equal(target.Id, edge.TargetId);
            Assert.True(source.IsAdjacent(target));
        }

        [Fact]
        public void Node_Couple_Throws_When_Edge_Is_Invalid()
        {
            var source = Node<Guid>.New(new GuidGenerator());
            var edge = new Edge(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), true);
            Assert.Throws<InvalidOperationException>(() => source.Couple(edge));
        }

        [Fact]
        public void Node_Decouple_Throws_When_Edge_Is_Invalid()
        {
            var source = Node<Guid>.New(new GuidGenerator());
            var edge = new Edge(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), true);
            Assert.Throws<InvalidOperationException>(() => source.Decouple(edge));
        }

        [Fact]
        public void Node_Decouple_Works_When_Node_Is_Source_Or_Target()
        {
            var node = Node<Guid>.New(new GuidGenerator());
            var edge = new Edge(Guid.NewGuid(), Guid.NewGuid(), node.Id, true);
            node.Couple(edge);
            Assert.True(node.IsAdjacent(edge.SourceId));
            node.Decouple(edge);
            Assert.False(node.IsAdjacent(edge.SourceId));

            edge = new Edge(Guid.NewGuid(), node.Id, Guid.NewGuid(), true);
            node.Couple(edge);
            Assert.True(node.IsAdjacent(edge.TargetId));
            node.Decouple(edge);
            Assert.False(node.IsAdjacent(edge.TargetId));
        }
    }
}
