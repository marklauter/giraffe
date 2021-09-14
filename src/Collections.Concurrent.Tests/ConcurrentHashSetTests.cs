using Xunit;

namespace Collections.Concurrent.Tests
{
    public class ConcurrentHashSetTests
    {
        [Fact]
        public void ConcurrentHashSet_Empty_IsEmpty()
        {
            Assert.True(ConcurrentHashSet<string>.Empty.IsEmpty);
        }

        [Fact]
        public void ConcurrentHashSet_IsReadOnly_Returns_False()
        {
            Assert.False(ConcurrentHashSet<string>.Empty.IsReadOnly);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_Contains_An_Added_Item()
        {
            var value = "v";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value));
            Assert.True(set.Contains(value));
        }

        [Fact]
        public void ConcurrentHashSet_DoesNot_Add_Duplicates()
        {
            var value = "v";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value));
            Assert.False(set.Add(value));
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_DoesNot_Contain_A_Removed_Item()
        {
            var value = "v";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value));
            Assert.True(set.Remove(value));
            Assert.False(set.Contains(value));
        }

        [Fact]
        public void ConcurrentHashSet_DoesNot_Remove_An_Item_Twice()
        {
            var value = "v";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value));
            Assert.True(set.Remove(value));
            Assert.False(set.Remove(value));
        }

        [Fact]
        public void ConcurrentHashSet_Clear_Removes_All_Items()
        {
            var value1 = "v1";
            var value2 = "v2";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.Equal(2, set.Count);
            set.Clear();
            Assert.True(set.IsEmpty);
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_Clone_Copies_All_Items()
        {
            var value1 = "v1";
            var value2 = "v2";
            var set = ConcurrentHashSet<string>.Empty;
            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));

            var clone = set.Clone() as ConcurrentHashSet<string>;

            Assert.Equal(2, clone.Count);
            Assert.True(clone.Contains(value1));
            Assert.True(clone.Contains(value2));
        }
    }
}
