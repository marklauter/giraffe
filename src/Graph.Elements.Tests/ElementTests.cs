using Graph.Classifiers;
using Graph.Elements;
using Graph.Qualifiers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Graph.Tests
{
    public sealed class ElementTests
    {
        [Fact]
        public void Node_New_Sets_Id()
        {
            var node = Node.New;
            Assert.NotEqual(Guid.Empty, node.Id);
        }

        [Fact]
        public void Element_Classify_Throws_ArgumentException()
        {
            var node = Node.New;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => node.Classify(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => node.Classify(label));

            label = null;
            Assert.Throws<ArgumentException>(() => node.Classify(label));
        }

        [Fact]
        public void Element_Classify_Returns_IElement()
        {
            var label = "x";
            var node = Node.New
                .Classify(label);
            Assert.True(node is IElement<Guid>);
        }

        [Fact]
        public void Element_Classify_Adds_Label()
        {
            var label = "x";
            var node = Node.New
                .Classify(label);
            Assert.True(node.Is(label));
        }

        [Fact]
        public void Element_Classify_Raises_ClassifiedEventArgs()
        {
            var label = "x";
            var node = Node.New;
            var e = Assert.Raises<ClassifiedEventArgs>(
                handler => node.Classified += handler,
                handler => node.Classified -= handler,
                () => node.Classify(label));
            Assert.Equal(node, e.Sender);
            Assert.Equal(label, e.Arguments.Label);
        }

        [Fact]
        public void Element_Declassify_Throws_ArgumentException()
        {
            var node = Node.New;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => node.Declassify(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => node.Declassify(label));

            label = null;
            Assert.Throws<ArgumentException>(() => node.Declassify(label));
        }

        [Fact]
        public void Element_Declassify_Removes_Label()
        {
            var label = "x";
            var node = Node.New
                .Classify(label);
            Assert.True(node.Is(label));
            node.Declassify(label);
            Assert.False(node.Is(label));
        }

        [Fact]
        public void Element_Declassify_Raises_DeclassifiedEventArgs()
        {
            var label = "x";
            var node = Node.New
                .Classify(label);
            Assert.True(node.Is(label));
            var e = Assert.Raises<DeclassifiedEventArgs>(
                handler => node.Declassified += handler,
                handler => node.Declassified -= handler,
                () => node.Declassify(label));
            Assert.Equal(node, e.Sender);
            Assert.Equal(label, e.Arguments.Label);
        }

        [Fact]
        public void Element_Is_Throws_ArgumentException()
        {
            var node = Node.New;

            var label = String.Empty;
            Assert.Throws<ArgumentException>(() => node.Is(label));

            label = " ";
            Assert.Throws<ArgumentException>(() => node.Is(label));

            label = null;
            Assert.Throws<ArgumentException>(() => node.Is(label));
        }

        [Fact]
        public void Element_Is_Enumerable_Throws_ArgumentNullException()
        {
            var node = Node.New;

            List<string> labels = null;
            Assert.Throws<ArgumentNullException>(() => node.Is(labels));
        }

        [Fact]
        public void Element_Is_Enumerable_Returns_True()
        {
            var node = Node.New
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var labels = new string[] { "1", "2" };
            Assert.True(node.Is(labels));
        }

        [Fact]
        public void Element_Is_Enumerable_Returns_False()
        {
            var node = Node.New
                .Classify("1")
                .Classify("2");

            var labels = new string[] { "2", "3" };
            Assert.False(node.Is(labels));
        }

        [Fact]
        public void Element_Clone_Is_Equal()
        {
            var node = Node.New
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var clone = node.Clone() as Node;

            Assert.NotNull(clone);
            Assert.True(clone.Is("1"));
            Assert.True(clone.Is("2"));
            Assert.True(clone.Is("3"));
        }

        [Fact]
        public void Element_JsonConstructor_Fills_Classes()
        {
            var node = Node.New
                .Classify("1")
                .Classify("2")
                .Classify("3");

            var json = JsonConvert.SerializeObject(node);
            var clone = JsonConvert.DeserializeObject<Node>(json);

            Assert.NotNull(clone);
            Assert.True(clone.Is("1"));
            Assert.True(clone.Is("2"));
            Assert.True(clone.Is("3"));
        }

        [Fact]
        public void Element_Qualify_Throws_ArgumentException()
        {
            var node = Node.New;

            var name = String.Empty;
            var value = "x";
            Assert.Throws<ArgumentException>(() => node.Qualify(name, value));

            name = " ";
            value = "x";
            Assert.Throws<ArgumentException>(() => node.Qualify(name, value));

            name = null;
            value = "x";
            Assert.Throws<ArgumentException>(() => node.Qualify(name, value));
        }

        [Fact]
        public void Element_Qualify_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = "y";
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Disqualify_Throws_ArgumentException()
        {
            var node = Node.New;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => node.Disqualify(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => node.Disqualify(name));

            name = null;
            Assert.Throws<ArgumentException>(() => node.Disqualify(name));
        }

        [Fact]
        public void Element_Disqualify_Removes_Attribute()
        {
            var name = "x";
            var value = "y";

            var node = Node.New
                .Qualify(name, value);

            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);

            node.Disqualify(name);
            Assert.False(node.TryGetProperty(name, out var v2));
            Assert.Null(v2);
        }

        [Fact]
        public void Element_HasProperty_Throws_ArgumentException()
        {
            var node = Node.New;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => node.HasProperty(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => node.HasProperty(name));

            name = null;
            Assert.Throws<ArgumentException>(() => node.HasProperty(name));
        }

        [Fact]
        public void Element_HasProperty_Returns_True()
        {
            var name = "x";
            var value = "y";
            var node = Node.New
                .Qualify(name, value);

            Assert.True(node.HasProperty(name));
        }

        [Fact]
        public void Element_HasProperty_Returns_False()
        {
            var name = "x";
            var node = Node.New;
            Assert.False(node.HasProperty(name));
        }

        [Fact]
        public void Element_Quality_Throws_ArgumentException()
        {
            var node = Node.New;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => node.TryGetProperty(name, out var _));

            name = " ";
            Assert.Throws<ArgumentException>(() => node.TryGetProperty(name, out var _));

            name = null;
            Assert.Throws<ArgumentException>(() => node.TryGetProperty(name, out var _));
        }

        [Fact]
        public void Element_Qualify_Raises_QualifiedEventArgs()
        {
            var name = "x";
            var value = "y";
            var node = Node.New;
            var args = Assert.Raises<QualifiedEventArgs>(
                handler => node.Qualified += handler,
                handler => node.Qualified -= handler,
                () => node.Qualify(name, value));
            Assert.Equal(node, args.Sender);
            Assert.Equal(name, args.Arguments.Name);
            Assert.Equal(value, args.Arguments.Value);
        }

        [Fact]
        public void Element_Disqualify_Does_Not_Raise_DisqualifiedEventArgs()
        {
            var name = "x";
            var node = Node.New;

            Assert.Throws<Xunit.Sdk.RaisesException>(() =>
            {
                _ = Assert.Raises<DisqualifiedEventArgs>(
                    handler => node.Disqualified += handler,
                    handler => node.Disqualified -= handler,
                    () => node.Disqualify(name));
            });
        }

        [Fact]
        public void Element_Disqualify_Raises_DisqualifiedEventArgs()
        {
            var value = 1;
            var name = "x";
            var node = Node.New;
            node.Qualify(name, value);

            var args = Assert.Raises<DisqualifiedEventArgs>(
                handler => node.Disqualified += handler,
                handler => node.Disqualified -= handler,
                () => node.Disqualify(name));
            Assert.Equal(node, args.Sender);
            Assert.Equal(name, args.Arguments.Name);
        }

        [Fact]
        public void Element_JsonConstructor_Fills_Attributes()
        {
            var node = Node.New
                .Qualify("x1", "y1")
                .Qualify("x2", "y2")
                .Qualify("x3", "y3");

            var json = JsonConvert.SerializeObject(node);
            var clone = JsonConvert.DeserializeObject<Node>(json);

            Assert.NotNull(clone);
            Assert.True(node.TryGetProperty("x1", out var cox1));
            Assert.True(clone.TryGetProperty("x1", out var clx1));
            Assert.Equal(cox1, clx1);
            Assert.True(node.TryGetProperty("x2", out var cox2));
            Assert.True(clone.TryGetProperty("x2", out var clx2));
            Assert.Equal(cox2, clx2);
            Assert.True(node.TryGetProperty("x3", out var cox3));
            Assert.True(clone.TryGetProperty("x3", out var clx3));
            Assert.Equal(cox3, clx3);
        }

        [Fact]
        public void Element_Clone_Fills_Attributes()
        {
            var node = Node.New
                .Qualify("x1", "y1")
                .Qualify("x2", "y2")
                .Qualify("x3", "y3");

            var clone = node.Clone() as Node;

            Assert.NotNull(clone);
            Assert.NotNull(clone);
            Assert.True(node.TryGetProperty("x1", out var cox1));
            Assert.True(clone.TryGetProperty("x1", out var clx1));
            Assert.Equal(cox1, clx1);
            Assert.True(node.TryGetProperty("x2", out var cox2));
            Assert.True(clone.TryGetProperty("x2", out var clx2));
            Assert.Equal(cox2, clx2);
            Assert.True(node.TryGetProperty("x3", out var cox3));
            Assert.True(clone.TryGetProperty("x3", out var clx3));
            Assert.Equal(cox3, clx3);
        }

        [Fact]
        public void Element_Qualify_Bool_False_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = false;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Bool_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = true;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_SByte_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = (sbyte)-1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Byte_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = (byte)1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Short_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = (short)1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_UShort_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = (ushort)1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Int_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_UInt_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1u;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Long_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1L;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_ULong_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1UL;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Float_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1.1f;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Double_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = 1.1d;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_Decimal_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = (decimal)1.1;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }

        [Fact]
        public void Element_Qualify_DateTime_True_Sets_Name_and_Value()
        {
            var node = Node.New;

            var name = "x";
            var value = DateTime.Now;
            node.Qualify(name, value);
            Assert.True(node.TryGetProperty(name, out var v));
            Assert.Equal(value, v);
        }
    }
}
