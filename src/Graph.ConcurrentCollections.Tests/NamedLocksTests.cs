using System;
using Xunit;

namespace Graph.ConcurrentCollections.Tests
{
    public class NamedLocksTests
    {
        [Fact]
        public void Empty_Returns_NamedLock()
        {
            Assert.NotNull(NamedLocks.Empty);
        }

        [Fact]
        public void Lock_Throws_On_Null_Empty_and_WhiteSpace()
        {
            var locks = NamedLocks.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => locks.Lock(name));

            name = null;
            Assert.Throws<ArgumentException>(() => locks.Lock(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => locks.Lock(name));
        }

        [Fact]
        public void LocksHeld_Throws_On_Null_Empty_and_WhiteSpace()
        {
            var locks = NamedLocks.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => locks.LocksHeld(name));

            name = null;
            Assert.Throws<ArgumentException>(() => locks.LocksHeld(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => locks.LocksHeld(name));
        }

        [Fact]
        public void Unlock_Throws_On_Null_Empty_and_WhiteSpace()
        {
            var locks = NamedLocks.Empty;

            var name = String.Empty;
            Assert.Throws<ArgumentException>(() => locks.Unlock(name));

            name = null;
            Assert.Throws<ArgumentException>(() => locks.Unlock(name));

            name = " ";
            Assert.Throws<ArgumentException>(() => locks.Unlock(name));
        }

        [Fact]
        public void Empty_LocksHeld_Returns_Zero()
        {
            var name = "lock";
            var locks = NamedLocks.Empty;
            Assert.Equal(0, locks.LocksHeld(name));
        }

        [Fact]
        public void After_Lock_LocksHeld_Returns_One()
        {
            var name = "lock";
            var locks = NamedLocks.Empty;
            locks.Lock(name);
            Assert.Equal(1, locks.LocksHeld(name));
        }

        [Fact]
        public void After_Unlock_LocksHeld_Returns_Zero()
        {
            var name = "lock";
            var locks = NamedLocks.Empty;
            locks.Lock(name);
            Assert.Equal(1, locks.LocksHeld(name));

            locks.Unlock(name);
            Assert.Equal(0, locks.LocksHeld(name));
        }

    }
}
