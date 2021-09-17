using Graphs.Classifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graphs.Elements.Tests
{
    public sealed class ClassifiersTests
    {
        [Fact]
        public void ClassifiedEventArgs_Constructor_Throws_ArgumentException()
        {
            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => new ClassifiedEventArgs(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => new ClassifiedEventArgs(label));

            label = null;
            Assert.Throws<ArgumentException>(() => new ClassifiedEventArgs(label));
        }

        [Fact]
        public void ClassifiedEventArgs_Constructor_Sets_Label()
        {
            var label = "x";
            var args = new ClassifiedEventArgs(label);
            Assert.Equal(label, args.Label);

            var id = Guid.NewGuid();
            var typeArgs = new ClassifiedEventArgs<Guid>(label, id);
            Assert.Equal(label, typeArgs.Label);
            Assert.Equal(id, typeArgs.Id);
        }

        [Fact]
        public void DeclassifiedEventArgs_Constructor_Throws_ArgumentException()
        {
            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => new DeclassifiedEventArgs(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => new DeclassifiedEventArgs(label));

            label = null;
            Assert.Throws<ArgumentException>(() => new DeclassifiedEventArgs(label));
        }

        [Fact]
        public void DeclassifiedEventArgs_Constructor_Sets_Label()
        {
            var label = "x";
            var args = new DeclassifiedEventArgs(label);
            Assert.Equal(label, args.Label);

            var id = Guid.NewGuid();
            var typeArgs = new DeclassifiedEventArgs<Guid>(label, id);
            Assert.Equal(label, typeArgs.Label);
            Assert.Equal(id, typeArgs.Id);
        }

        [Fact]
        public void ClassificationCollection_Empty_Returns_Empty_Collection()
        {
            var collection = ClassificationCollection.Empty;
            Assert.Empty(collection);
        }

        [Fact]
        public void ClassificationCollection_Classify_Throws_ArgumentException()
        {
            var collection = ClassificationCollection.Empty;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Classify(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => collection.Classify(label));

            label = null;
            Assert.Throws<ArgumentException>(() => collection.Classify(label));
        }

        [Fact]
        public void ClassificationCollection_Classify_Adds_Label()
        {
            var label = "x";
            var collection = ClassificationCollection.Empty;
            collection.Classify(label);
            Assert.Contains(label, collection);
            Assert.True(collection.Is(label));
        }

        [Fact]
        public void ClassificationCollection_Classify_Raises_ClassifiedEventArgs()
        {
            var label = "x";
            var collection = ClassificationCollection.Empty;
            var e = Assert.Raises<ClassifiedEventArgs>(
                handler => collection.Classified += handler,
                handler => collection.Classified -= handler,
                () => collection.Classify(label));
            Assert.Equal(collection, e.Sender);
            Assert.Equal(label, e.Arguments.Label);
        }

        [Fact]
        public void ClassificationCollection_Declassify_Throws_ArgumentException()
        {
            var collection = ClassificationCollection.Empty;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Declassify(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => collection.Declassify(label));

            label = null;
            Assert.Throws<ArgumentException>(() => collection.Declassify(label));
        }

        [Fact]
        public void ClassificationCollection_Declassify_Removes_Label()
        {
            var label = "x";
            var collection = ClassificationCollection.Empty;
            collection.Classify(label);
            Assert.True(collection.Is(label));
            collection.Declassify(label);
            Assert.False(collection.Is(label));
        }

        [Fact]
        public void ClassificationCollection_Declassify_Raises_DeclassifiedEventArgs()
        {
            var label = "x";
            var collection = ClassificationCollection.Empty;
            collection.Classify(label);
            Assert.True(collection.Is(label));
            var e = Assert.Raises<DeclassifiedEventArgs>(
                handler => collection.Declassified += handler,
                handler => collection.Declassified -= handler,
                () => collection.Declassify(label));
            Assert.Equal(collection, e.Sender);
            Assert.Equal(label, e.Arguments.Label);
        }

        [Fact]
        public void ClassificationCollection_Is_Throws_ArgumentException()
        {
            var collection = ClassificationCollection.Empty;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Is(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => collection.Is(label));

            label = null;
            Assert.Throws<ArgumentException>(() => collection.Is(label));
        }

        [Fact]
        public void ClassificationCollection_Is_Enumerable_Throws_ArgumentNullException()
        {
            var collection = ClassificationCollection.Empty;

            List<string> labels = null;
            Assert.Throws<ArgumentNullException>(() => collection.Is(labels));
        }

        [Fact]
        public void ClassificationCollection_Is_Enumerable_Returns_True()
        {
            var collection = ClassificationCollection.Empty;
            _ = collection
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var labels = new string[] { "1", "2" };
            Assert.True(collection.Is(labels));
        }

        [Fact]
        public void ClassificationCollection_Is_Enumerable_Returns_False()
        {
            var collection = ClassificationCollection.Empty;
            _ = collection
                .Classify("1")
                .Classify("2");

            var labels = new string[] { "2", "3" };
            Assert.False(collection.Is(labels));
        }

        [Fact]
        public void ClassificationCollection_Clone_Is_Equal()
        {
            var collection = ClassificationCollection.Empty;
            _ = collection
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var clone = collection.Clone() as ClassificationCollection;

            Assert.NotNull(clone);
            Assert.True(collection.Is(clone));
            Assert.True(clone.Is(collection));
        }

        [Fact]
        public void ClassificationCollection_JsonConstructor_Fills_Classes()
        {
            var collection = ClassificationCollection.Empty;
            _ = collection
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var json = JsonConvert.SerializeObject(collection);
            var clone = JsonConvert.DeserializeObject<ClassificationCollection>(json);

            Assert.NotNull(clone);
            Assert.True(collection.Is(clone));
            Assert.True(clone.Is(collection));
        }
    }
}
