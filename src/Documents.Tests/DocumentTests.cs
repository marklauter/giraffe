using System;
using Xunit;

namespace Documents.Tests
{
    public class DocumentTests
    {
        [Fact]
        public void Document_ExplicitOperatorT_ReturnsDocumentT()
        {
            var member = new Member();
            var document1 = (Document<Member>)member;
            Assert.NotNull(document1);
            Assert.NotNull(document1.Member);
            Assert.Equal(typeof(Member), document1.Member.GetType());
            Assert.Equal(member.Id.ToString(), document1.Key);

            var keylessMember = new KeylessMember();
            var document2 = (Document<KeylessMember>)keylessMember;
            Assert.NotNull(document2);
            Assert.NotNull(document2.Member);
            Assert.Equal(typeof(KeylessMember), document2.Member.GetType());
            Assert.Equal(keylessMember.GetHashCode().ToString(), document2.Key);
        }
    }
}
