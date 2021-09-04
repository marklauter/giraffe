using Newtonsoft.Json;
using Xunit;

namespace Graph.ConcurrentCollections.Tests
{
    public class ConcurrentHashSetTests
    {
        [Fact]
        public void ConcurrentHashSet_Add()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.False(hashset.Add(1));
            Assert.True(hashset.Add(2));
            Assert.False(hashset.Add(2));
        }

        [Fact]
        public void ConcurrentHashSet_Clear()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.False(hashset.Contains(1));
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Contains(1));
            hashset.Clear();
            Assert.Empty(hashset);
        }

        [Fact]
        public void ConcurrentHashSet_Contains()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.False(hashset.Contains(1));
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Contains(1));
            Assert.True(hashset.Remove(1));
            Assert.False(hashset.Contains(1));
        }

        [Fact]
        public void ConcurrentHashSet_Count()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));
            Assert.Equal(2, hashset.Count);
        }

        [Fact]
        public void ConcurrentHashSet_EnsureCapacity()
        {
            var capacity = 20;
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(capacity <= hashset.EnsureCapacity(capacity));
        }

        [Fact]
        public void ConcurrentHashSet_Enumerate()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));
            foreach(var item in hashset)
            {
                Assert.InRange(item, 0, 3);
            }
        }

        [Fact]
        public void ConcurrentHashSet_ExceptWith()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(2));

            hashset.ExceptWith(other);
            Assert.True(hashset.Contains(1));
            Assert.False(hashset.Contains(2));
            Assert.False(other.Contains(1));
        }

        [Fact]
        public void ConcurrentHashSet_IntersectWith()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(2));
            Assert.True(other.Add(3));

            hashset.IntersectWith(other);

            Assert.False(hashset.Contains(1));
            Assert.True(hashset.Contains(2));
            Assert.False(other.Contains(1));
            Assert.True(other.Contains(2));
            Assert.True(other.Contains(3));
        }

        [Fact]
        public void ConcurrentHashSet_IsSubsetOf()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(1));

            Assert.False(hashset.IsSubsetOf(other));
            Assert.True(other.IsSubsetOf(hashset));
        }

        [Fact]
        public void ConcurrentHashSet_IsSupersetOf()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(1));

            Assert.True(hashset.IsSupersetOf(other));
            Assert.False(other.IsSupersetOf(hashset));
        }

        [Fact]
        public void ConcurrentHashSet_Overlaps()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(2));
            Assert.True(other.Add(3));

            Assert.True(hashset.Overlaps(other));
            Assert.True(other.Overlaps(hashset));
        }

        [Fact]
        public void ConcurrentHashSet_Remove()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.False(hashset.Remove(1));
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Remove(1));
            Assert.True(hashset.Add(1));
        }

        [Fact]
        public void ConcurrentHashSet_SetEquals()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));
            Assert.True(hashset.Add(2));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(1));
            Assert.True(other.Add(2));

            Assert.True(hashset.SetEquals(other));
            Assert.True(other.SetEquals(hashset));
        }

        [Fact]
        public void ConcurrentHashSet_UnionWith()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));

            var other = new ConcurrentHashSet<int>();
            Assert.True(other.Add(2));
            Assert.False(hashset.Contains(2));

            hashset.UnionWith(other);
            Assert.True(hashset.Contains(2));
            Assert.False(other.Contains(1));
        }

        [Fact]
        public void ConcurrentHashSet_Serialization()
        {
            var hashset = new ConcurrentHashSet<int>();
            Assert.True(hashset.Add(1));

            var json = JsonConvert.SerializeObject(hashset);
            var other = JsonConvert.DeserializeObject<ConcurrentHashSet<int>>(json);

            Assert.True(hashset.SetEquals(other));
            
        }
    }
}
