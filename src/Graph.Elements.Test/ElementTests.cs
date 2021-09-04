using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Graph.Elements.Test
{
    public class ElementTests
    {
        [Fact]
        public void New_Element_Has_Id()
        {
            var element = new Element();
            Assert.NotEqual(Guid.Empty, element.Id);
        }

        [Fact]
        public void New_Element_Has_Labels()
        {
            var element = new Element();
            Assert.Empty(element.Labels);
        }

        [Fact]
        public void New_Element_Has_Elements()
        {
            var element = new Element();
            Assert.NotNull(element.Qualify("x", "y"));
        }

        [Fact]
        public void New_Element_Classify_Adds_Label()
        {
            var label = "x";

            var element = new Element();
            Assert.Empty(element.Labels);
            Assert.NotNull(element.Classify(label));
            Assert.NotEmpty(element.Labels);
            Assert.True(element.Is(label));
        }

        [Fact]
        public void New_Element_Declassify_Removes_Label()
        {
            var label = "x";

            var element = new Element();
            Assert.Empty(element.Labels);
            Assert.NotNull(element.Classify(label));
            Assert.NotEmpty(element.Labels);
            Assert.True(element.Is(label));

            Assert.NotNull(element.Declassify(label));
            Assert.Empty(element.Labels);
            Assert.False(element.Is(label));
        }

        [Fact]
        public void New_Element_Classify_Adds_Labels()
        {
            var labels = new string[] { "x", "x", "y" };

            var element = new Element();
            Assert.Empty(element.Labels);
            Assert.NotNull(element.Classify(labels));
            Assert.NotEmpty(element.Labels);
            Assert.Equal(2, element.Labels.Count());
            Assert.True(element.Is(labels[0]));
            Assert.True(element.Is(labels[1]));
            Assert.True(element.Is(labels[2]));
        }

        [Fact]
        public void New_Element_Qualify_Adds_Attibute()
        {
            var attribute = new KeyValuePair<string, string>("x", "y");

            var element = new Element();
            Assert.Empty(element.Labels);
            Assert.NotNull(element.Qualify(attribute.Key, attribute.Value));
            Assert.True(element.HasAttribute(attribute.Key));
            Assert.True(element.TryGetAttribute(attribute.Key, out var value));
            Assert.Equal(attribute.Value, value);
        }

        [Fact]
        public void New_Element_Qualify_Adds_Attibutes()
        {
            var attributes = new KeyValuePair<string, string>[] 
            {
                new KeyValuePair<string, string>("x1", "y1"),
                new KeyValuePair<string, string>("x2", "y2"),
            };

            var element = new Element();
            Assert.Empty(element.Labels);
            Assert.NotNull(element.Qualify(attributes));
            foreach(var attribute in attributes)
            {
                Assert.True(element.HasAttribute(attribute.Key));
                Assert.True(element.TryGetAttribute(attribute.Key, out var value));
                Assert.Equal(attribute.Value, value);
            }
        }

        [Fact]
        public void New_Element_Clone_Copies_Id_Labels_and_Attributes()
        {
            var labels = new string[] { "x", "x", "y" };
            var attributes = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("x1", "y1"),
                new KeyValuePair<string, string>("x2", "y2"),
            };

            var element = new Element();
            var elementHashCode = element.GetHashCode();

            Assert.Empty(element.Labels);
            Assert.NotNull(element.Classify(labels));
            Assert.NotEmpty(element.Labels);
            Assert.Equal(2, element.Labels.Count());
            Assert.True(element.Is(labels[0]));
            Assert.True(element.Is(labels[1]));
            Assert.True(element.Is(labels[2]));
            Assert.NotNull(element.Qualify(attributes));
            foreach (var attribute in attributes)
            {
                Assert.True(element.HasAttribute(attribute.Key));
                Assert.True(element.TryGetAttribute(attribute.Key, out var value));
                Assert.Equal(attribute.Value, value);
            }

            var clone = element.Clone() as Element;
            var cloneHashCode = clone.GetHashCode();
            Assert.Equal(element.Id, clone.Id);
            Assert.Equal(2, clone.Labels.Count());
            Assert.True(clone.Is(labels[0]));
            Assert.True(clone.Is(labels[1]));
            Assert.True(clone.Is(labels[2]));
            foreach (var attribute in attributes)
            {
                Assert.True(clone.HasAttribute(attribute.Key));
                Assert.True(clone.TryGetAttribute(attribute.Key, out var value));
                Assert.Equal(attribute.Value, value);
            }

            Assert.Equal(elementHashCode, cloneHashCode);
        }

        [Fact]
        public void New_Element_Equals_Returns_True()
        {
            var element = new Element();
            var clone = element.Clone() ;

            Assert.True(element.Equals(clone));
            Assert.True(element.Equals(clone as Element));
        }

        [Fact]
        public void New_Element_Equals_Returns_False()
        {
            var element = new Element();
            var other = new Element();

            Assert.False(element.Equals(null as object));
            Assert.False(element.Equals(other as object));
            
            Assert.False(element.Equals(null));
            Assert.False(element.Equals(other));
        }
    }
}
