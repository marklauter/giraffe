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
    }
}
