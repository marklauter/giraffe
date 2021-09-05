using System;
using Xunit;

namespace Graph.Elements.Test
{
    public class ElementIndexTests
    {
        [Fact]
        public void Empty_ElementIndex_NotNull()
        {
            Assert.NotNull(ElementIndex.Empty);
        }

        [Fact]
        public void When_Empty_Count_Returns_Zero()
        {
            Assert.Equal(0, ElementIndex.Empty.Count());
        }

        [Fact]
        public void Count_Returns_1_After_Add()
        {
            var element = new ConcreteElement();
            var index = ElementIndex.Empty;

            index.Add(element);
            Assert.Equal(1, index.Count());
        }

        [Fact]
        public void Count_Returns_1_After_Add_With_Multiple_Labels()
        {
            var element = new ConcreteElement()
                .Classify("one")
                .Classify("two");

            var index = ElementIndex.Empty;

            index.Add(element);
            Assert.Equal(1, index.Count());
        }

        [Fact]
        public void Count_Returns_1_After_Add_and_Lables_Change()
        {
            var element = new ConcreteElement()
                .Classify("one");

            var index = ElementIndex.Empty;

            index.Add(element);
            Assert.Equal(1, index.Count());

            element.Classify("two");
            Assert.Equal(1, index.Count());
        }

        [Fact]
        public void Element_Classify_Rasies_Index_Updated()
        {
            var element = new ConcreteElement();
            var index = ElementIndex.Empty;

            index.Add(element);
            Assert.Equal(1, index.Count());

            Assert.Raises<IndexUpdatedEventArgs>(
                handler => index.Updated += handler,
                handler => index.Updated -= handler,
                () =>
                {
                    element.Classify("one");
                });
            Assert.Equal(1, index.Count());
        }

        [Fact]
        public void Element_Declassify_Rasies_Index_Updated()
        {
            var element = new ConcreteElement()
                .Classify("one");
            var index = ElementIndex.Empty;

            index.Add(element);
            Assert.Equal(1, index.Count());

            Assert.Raises<IndexUpdatedEventArgs>(
                handler => index.Updated += handler,
                handler => index.Updated -= handler,
                () =>
                {
                    element.Declassify("one");
                });
            Assert.Equal(1, index.Count());
        }

        [Fact]
        public void Add_Null_Throws()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                ElementIndex.Empty.Add(null);
            });
        }

        [Fact]
        public void TryGetElements_Throws_On_Null_Empty_and_WhiteSpace()
        {
            var label = String.Empty;
            Assert.Throws<ArgumentException>(() =>
            {
                ElementIndex.Empty.TryGetElements(label, out var _);
            });

            label = null;
            Assert.Throws<ArgumentException>(() =>
            {
                ElementIndex.Empty.TryGetElements(label, out var _);
            });

            label = " ";
            Assert.Throws<ArgumentException>(() =>
            {
                ElementIndex.Empty.TryGetElements(label, out var _);
            });
        }

        [Fact]
        public void TryGetElements_Returns_False_When_No_Match()
        {
            var label = "one";
            Assert.False(ElementIndex.Empty.TryGetElements(label, out var _));
        }

        [Fact]
        public void TryGetElements_Returns_True_When_Match()
        {
            var label = "one";
            var element = new ConcreteElement()
                .Classify(label);
            var index = ElementIndex.Empty;
            index.Add(element);
            Assert.True(index.TryGetElements(label, out var elements));
            Assert.Contains(element.Id, elements);
        }
    }
}
