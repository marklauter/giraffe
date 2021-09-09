using Graph.Qualifiers;
using Newtonsoft.Json;
using System;
using Xunit;

namespace Graph.Tests
{
    public sealed class QualifiersTests
    {
        [Fact]
        public void QualifiedEventArgs_Constructor_Throws_ArgumentException()
        {
            var name = String.Empty;
            var v1 = "1";
            Assert.Throws<ArgumentException>(() => new QualifiedEventArgs(name, v1));

            name = " ";
            var v2 = 2;
            Assert.Throws<ArgumentException>(() => new QualifiedEventArgs(name, v2));

            name = null;
            var v3 = 3.3;
            Assert.Throws<ArgumentException>(() => new QualifiedEventArgs(name, v3));
        }

        [Fact]
        public void QualifiedEventArgs_Constructor_Sets_Name_and_Value()
        {
            var name = "x";
            var value = "y";
            var args = new QualifiedEventArgs(name, value);
            Assert.Equal(name, args.Name);
            Assert.Equal(value, args.Value);
        }

        [Fact]
        public void DisqualifiedEventArgs_Constructor_Throws_ArgumentException()
        {
            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => new DisqualifiedEventArgs(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => new DisqualifiedEventArgs(name));

            name = null;
            Assert.Throws<ArgumentException>(() => new DisqualifiedEventArgs(name));
        }

        [Fact]
        public void DisqualifiedEventArgs_Constructor_Sets_Name_and_Value()
        {
            var name = "x";
            var args = new DisqualifiedEventArgs(name);
            Assert.Equal(name, args.Name);
        }

        [Fact]
        public void QualificationCollection_Empty_Returns_Empty_Collection()
        {
            var collection = AttributeCollection.Empty;
            Assert.Empty(collection);
        }

        [Fact]
        public void QualificationCollection_Qualify_Throws_ArgumentException()
        {
            var collection = AttributeCollection.Empty;

            var name = String.Empty;
            var value = "x";
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));

            name = " ";
            value = "x";
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));

            name = null;
            value = "x";
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));

            name = "x";
            value = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));

            name = "x";
            value = " ";
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));

            name = "x";
            value = null;
            Assert.Throws<ArgumentException>(() => collection.Qualify(name, value));
        }

        [Fact]
        public void QualificationCollection_Qualify_Sets_Name_and_Value()
        {
            var collection = AttributeCollection.Empty;

            var name = "x";
            var value = "y";
            collection.Qualify(name, value);
            Assert.True(collection.TryGetValue(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void QualificationCollection_Disqualify_Throws_ArgumentException()
        {
            var collection = AttributeCollection.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Disqualify(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => collection.Disqualify(name));

            name = null;
            Assert.Throws<ArgumentException>(() => collection.Disqualify(name));
        }

        [Fact]
        public void QualificationCollection_Disqualify_Removes_Attribute()
        {
            var collection = AttributeCollection.Empty;

            var name = "x";
            var value = "y";
            collection.Qualify(name, value);

            Assert.True(collection.TryGetValue(name, out var v));
            Assert.Equal(value, v);
            Assert.Contains((name, (SerializableValue)value), collection);

            collection.Disqualify(name);
            Assert.False(collection.TryGetValue(name, out var v2));
            Assert.Null(v2);
            Assert.DoesNotContain((name, (SerializableValue)value), collection);
        }

        [Fact]
        public void QualificationCollection_HasQuality_Throws_ArgumentException()
        {
            var collection = AttributeCollection.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.HasQuality(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => collection.HasQuality(name));

            name = null;
            Assert.Throws<ArgumentException>(() => collection.HasQuality(name));
        }

        [Fact]
        public void QualificationCollection_HasQuality_Returns_True()
        {
            var collection = AttributeCollection.Empty;

            var name = "x";
            var value = "y";
            collection.Qualify(name, value);

            Assert.True(collection.HasQuality(name));
        }

        [Fact]
        public void QualificationCollection_HasQuality_Returns_False()
        {
            var collection = AttributeCollection.Empty;

            var name = "x";
            Assert.False(collection.HasQuality(name));
        }

        [Fact]
        public void QualificationCollection_Quality_Throws_ArgumentException()
        {
            var collection = AttributeCollection.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.TryGetValue(name, out var _));

            name = " ";
            Assert.Throws<ArgumentException>(() => collection.TryGetValue(name, out var _));

            name = null;
            Assert.Throws<ArgumentException>(() => collection.TryGetValue(name, out var _));
        }

        [Fact]
        public void QualificationCollection_Qualify_Raises_QualifiedEventArgs()
        {
            var name = "x";
            var value = "y";
            var collection = AttributeCollection.Empty;
            var args = Assert.Raises<QualifiedEventArgs>(
                handler => collection.Qualified += handler,
                handler => collection.Qualified -= handler,
                () => collection.Qualify(name, value));
            Assert.Equal(collection, args.Sender);
            Assert.Equal(name, args.Arguments.Name);
            Assert.Equal(value, args.Arguments.Value);
        }

        [Fact]
        public void QualificationCollection_Disqualify_Raises_DisqualifiedEventArgs()
        {
            var name = "x";
            var collection = AttributeCollection.Empty;
            var args = Assert.Raises<DisqualifiedEventArgs>(
                handler => collection.Disqualified += handler,
                handler => collection.Disqualified -= handler,
                () => collection.Disqualify(name));
            Assert.Equal(collection, args.Sender);
            Assert.Equal(name, args.Arguments.Name);
        }

        [Fact]
        public void QualificationCollection_JsonConstructor_Fills_Attributes()
        {
            var collection = AttributeCollection.Empty;
            _ = collection
                .Qualify("x1", "y1")
                .Qualify("x2", "y2")
                .Qualify("x3", "y3");

            var json = JsonConvert.SerializeObject(collection);
            var clone = JsonConvert.DeserializeObject<AttributeCollection>(json);

            Assert.NotNull(clone);
            Assert.True(collection.TryGetValue("x1", out var cox1));
            Assert.True(clone.TryGetValue("x1", out var clx1));
            Assert.Equal(cox1, clx1);
            Assert.True(collection.TryGetValue("x2", out var cox2));
            Assert.True(clone.TryGetValue("x2", out var clx2));
            Assert.Equal(cox2, clx2);
            Assert.True(collection.TryGetValue("x3", out var cox3));
            Assert.True(clone.TryGetValue("x3", out var clx3));
            Assert.Equal(cox3, clx3);
        }

        [Fact]
        public void QualificationCollection_Clone_Fills_Attributes()
        {
            var collection = AttributeCollection.Empty;
            _ = collection
                .Qualify("x1", "y1")
                .Qualify("x2", "y2")
                .Qualify("x3", "y3");

            var clone = collection.Clone() as AttributeCollection;

            Assert.NotNull(clone);
            Assert.NotNull(clone);
            Assert.True(collection.TryGetValue("x1", out var cox1));
            Assert.True(clone.TryGetValue("x1", out var clx1));
            Assert.Equal(cox1, clx1);
            Assert.True(collection.TryGetValue("x2", out var cox2));
            Assert.True(clone.TryGetValue("x2", out var clx2));
            Assert.Equal(cox2, clx2);
            Assert.True(collection.TryGetValue("x3", out var cox3));
            Assert.True(clone.TryGetValue("x3", out var clx3));
            Assert.Equal(cox3, clx3);
        }
    }
}
