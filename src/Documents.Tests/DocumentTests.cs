using System;
using System.Linq;
using Xunit;

namespace Documents.Tests
{
    public sealed class DocumentTests
    {
        [Fact]
        public void Document_ExplicitOperator_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => (Document<KeylessMember>)new KeylessMember());
        }

        [Fact]
        public void Document_ExplicitOperator_Throws_ArgumentNullException()
        {
            var member = null as Member;
            Assert.Throws<ArgumentNullException>(() => (Document<Member>)member);
        }

        [Fact]
        public void Document_ExplicitOperatorT_ReturnsDocumentT()
        {
            var member = new Member();
            var document = (Document<Member>)member;
            Assert.NotNull(document);
            Assert.NotNull(document.Member);
            Assert.Equal(typeof(Member), document.Member.GetType());
            Assert.Equal(member.Id.ToString(), document.Key);
        }

        [Fact]
        public void Document_Explicit_Member_Operator_Returns_Member()
        {
            var member = new Member();
            var document = (Document<Member>)member;
            Assert.Equal(member, (Member)document);
        }

        [Fact]
        public void Document_Clone_Copies_Member()
        {
            var member = new Member();
            var document = (Document<Member>)member;
            var clone = document.Clone() as Document<Member>;
            Assert.NotNull(clone.Member);
            Assert.Equal(document.Key, clone.Key);
            Assert.Equal(document.ETag, clone.ETag);
            Assert.Equal(typeof(Member), clone.Member.GetType());
            Assert.Equal(member.Id.ToString(), clone.Key);
            Assert.Equal(document, clone);
        }

        [Fact]
        public void Document_Equals_False_When_Null()
        {
            var document = (Document<Member>)new Member();
            var clone = null as Document<Member>;
            Assert.False(document.Equals(clone));
        }

        [Fact]
        public void Document_Equals_X_Y_False_When_Null()
        {
            var document = (Document<Member>)new Member();
            var clone = null as Document<Member>;
            Assert.False(document.Equals(document, clone));
            Assert.False(document.Equals(clone, document));
        }

        [Fact]
        public void Document_Equals_X_Y_True_When_Equal()
        {
            var document = (Document<Member>)new Member();
            var clone = document.Clone() as Document<Member>;
            Assert.True(document.Equals(document, clone));
            Assert.True(document.Equals(clone, document));
        }

        [Fact]
        public void Document_Equals_Obj_True_When_Equal()
        {
            var document = (Document<Member>)new Member();
            var clone = document.Clone();
            Assert.True(document.Equals(clone));
        }

        [Fact]
        public void Document_Equals_Obj_False_When_Null()
        {
            var document = (Document<Member>)new Member();
            var clone = null as object;
            Assert.False(document.Equals(clone));
        }

        [Fact]
        public void Document_Equals_Obj_False_When_Not_Document()
        {
            var document = (Document<Member>)new Member();
            var clone = new object();
            Assert.False(document.Equals(clone));
        }

        [Fact]
        public void Document_Equals_GetHashCode_Is_Consistent()
        {
            var document = (Document<Member>)new Member();
            Assert.Equal(document.GetHashCode(), document.GetHashCode(document));
        }

        [Fact]
        public void Document_Equals_GetHashCode_Obj_Throws_When_Null()
        {
            var document = (Document<Member>)new Member();
            Assert.Throws<ArgumentNullException>(() => document.GetHashCode(null));
        }

        [Fact]
        public void Document_Keys_Caches_Keys()
        {
            Assert.Contains("Id", KeyBuilder<Member>.KeyProperties.Select(pi => pi.Name));
        }
    }
}
