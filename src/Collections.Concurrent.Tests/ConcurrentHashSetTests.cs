using System.Collections.Generic;
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_As_ICollection_Contains_An_Added_Item()
        {
            var value = "v";
            var set = ConcurrentHashSet<string>.Empty;
            (set as ICollection<string>).Add(value);
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

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_ExceptWith_Excludes_Items()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            set.ExceptWith(new string[] { value2, value3 });

            Assert.True(set.Contains(value1));
            Assert.False(set.Contains(value2));
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_IntersectWith_Leaves_Only_Mutal_Items()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            set.IntersectWith(new string[] { value2, value3 });

            Assert.False(set.Contains(value1));
            Assert.True(set.Contains(value2));
            Assert.False(set.Contains(value3));
        }

        [Fact]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2017:Do not use Contains() to check if a value exists in a collection", Justification = "Testing the custom implementation of Contains.")]
        public void ConcurrentHashSet_UnionWith_Joins_All_Items()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            set.UnionWith(new string[] { value2, value3 });

            Assert.True(set.Contains(value1));
            Assert.True(set.Contains(value2));
            Assert.True(set.Contains(value3));
        }

        [Fact]
        public void ConcurrentHashSet_Set_Equals_Returns_True()
        {
            var value1 = "v1";
            var value2 = "v2";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));

            Assert.True(set.SetEquals(new string[] { value1, value2 }));
        }

        [Fact]
        public void ConcurrentHashSet_Except_Returns_Set_Difference()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            var except = set.Except(new string[] { value2, value3 });

            Assert.Contains(value1, except);
            Assert.DoesNotContain(value2, except);
            Assert.DoesNotContain(value3, except);
        }

        [Fact]
        public void ConcurrentHashSet_SymmetricExcept_Returns_Set_Difference()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            set.SymmetricExceptWith(new string[] { value2, value3 });

            Assert.Contains(value1, set);
            Assert.DoesNotContain(value2, set);
            Assert.Contains(value3, set);
        }

        [Fact]
        public void ConcurrentHashSet_Enumerator_Enumerates()
        {
            var value1 = "v1";
            var value2 = "v2";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));

            var i = 0;
            foreach (var item in set)
            {
                ++i;
            }

            Assert.Equal(2, i);
        }

        [Fact]
        public void ConcurrentHashSet_As_IEnumerable_Enumerator_Enumerates()
        {
            var value1 = "v1";
            var value2 = "v2";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));

            var ienum = (set as IEnumerable<string>);
            var e = ienum.GetEnumerator();
            Assert.Null(e.Current);
            Assert.True(e.MoveNext());
            Assert.Equal(value1, e.Current);
            Assert.True(e.MoveNext());
            Assert.Equal(value2, e.Current);
            Assert.False(e.MoveNext());
        }

        [Fact]
        public void ConcurrentHashSet_IsSubsetOf()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.True(set.IsSubsetOf(new string[] { value1, value2, value3 }));
        }

        [Fact]
        public void ConcurrentHashSet_IsSupersetOf()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.True(set.Add(value3));
            Assert.True(set.IsSupersetOf(new string[] { value2 }));
        }

        [Fact]
        public void ConcurrentHashSet_IsProperSubsetOf()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.True(set.IsProperSubsetOf(new string[] { value1, value2, value3 }));
        }

        [Fact]
        public void ConcurrentHashSet_IsProperSupersetOf()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.True(set.Add(value3));
            Assert.True(set.IsProperSupersetOf(new string[] { value2 }));
        }

        [Fact]
        public void ConcurrentHashSet_Overlaps()
        {
            var value1 = "v1";
            var value2 = "v2";
            var value3 = "v3";
            var set = ConcurrentHashSet<string>.Empty;

            Assert.True(set.Add(value1));
            Assert.True(set.Add(value2));
            Assert.True(set.Overlaps(new string[] { value2, value3 }));
        }
    }
}
