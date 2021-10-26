using Documents.Collections;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Documents.Cache.Tests
{
    public sealed class CachedDocumentCollectionTests
        : IClassFixture<CachedDocumentTestsFixture>
    {
#pragma warning disable IDE1006 // Naming Styles
        private static CachedDocumentCollection<Member> GetCollection()
        {
            var cacheOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            };

            var documentCache = new MemoryDocumentCache<Member>(cacheOptions);
            var documentCollection = HeapDocumentCollection<Member>.Empty;

            return new CachedDocumentCollection<Member>(documentCollection, documentCache);
        }

        [Fact]
        public void Collection_Empty_Contains_Zero_Items()
        {
            var collection = GetCollection();
            Assert.Equal(0, collection.Count);
            Assert.True(collection.IsEmpty);
        }

        [Fact]
        public Task Collection_Add_Single_Throws_ArgumentNullException()
        {
            var collection = GetCollection();
            return Assert.ThrowsAsync<ArgumentNullException>(() => collection.AddAsync(null as Document<Member>));
        }

        [Fact]
        public Task Collection_Add_Multiple_Throws_ArgumentNullException()
        {
            var collection = GetCollection();
            return Assert.ThrowsAsync<ArgumentNullException>(() => collection.AddAsync(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public async Task Collection_Add_Changes_Count()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);

            Assert.Equal(1, collection.Count);
            Assert.True(await collection.ContainsAsync(document));
        }

        [Fact]
        public async Task Collection_Add_Multiple_Changes_Count()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                document
            };

            await collection.AddAsync(documents);
            Assert.Equal(documents.Length, collection.Count);
            Assert.True(await collection.ContainsAsync(document));
        }

        [Fact]
        public async Task Collection_Add_Raises_DocumentAdded()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var e = await Assert.RaisesAsync<DocumentAddedEventArgs<Member>>(
                h => collection.DocumentAdded += h,
                h => collection.DocumentAdded -= h,
                () => collection.AddAsync(document));
            Assert.Equal(collection, e.Sender);
            Assert.Equal(document, e.Arguments.Document);
        }

        [Fact]
        public async Task Collection_Contains_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ContainsAsync(key));

            key = null;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ContainsAsync(key));

            key = " ";
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ContainsAsync(key));

            await Assert.ThrowsAsync<ArgumentNullException>(() => collection.ContainsAsync(null as Document<Member>));
        }

        [Fact]
        public async Task Collection_Contains_Document_Returns_True()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);

            Assert.True(await collection.ContainsAsync(document));
        }

        [Fact]
        public async Task Collection_Contains_Document_Returns_False()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);

            Assert.False(await collection.ContainsAsync((Document<Member>)new Member()));
        }

        [Fact]
        public async Task Collection_Contains_Key_Returns_True()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);

            Assert.True(await collection.ContainsAsync(document.Key));
        }

        [Fact]
        public async Task Collection_Contains_Key_Returns_False()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);

            Assert.False(await collection.ContainsAsync(((Document<Member>)new Member()).Key));
        }

        [Fact]
        public async Task Collection_Clear_Empties_Collection_And_Raises_Cleared()
        {
            var collection = GetCollection();

            await collection.AddAsync((Document<Member>)new Member());
            await collection.AddAsync((Document<Member>)new Member());
            await collection.AddAsync((Document<Member>)new Member());

            Assert.Equal(3, collection.Count);
            var e = await Assert.RaisesAsync<EventArgs>(
                h => collection.Cleared += h,
                h => collection.Cleared -= h,
                () => collection.ClearAsync());
            Assert.Equal(collection, e.Sender);
            Assert.True(collection.IsEmpty);
        }

        [Fact]
        public async Task Collection_Clear_Empties_Collection()
        {
            var collection = GetCollection();

            await collection.AddAsync((Document<Member>)new Member());
            await collection.AddAsync((Document<Member>)new Member());
            await collection.AddAsync((Document<Member>)new Member());
            await collection.ClearAsync();
            Assert.True(collection.IsEmpty);
        }

        [Fact]
        public async Task Collection_Remove_Single_Key_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.RemoveAsync(key));

            key = null;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.RemoveAsync(key));

            key = " ";
            await Assert.ThrowsAsync<ArgumentException>(() => collection.RemoveAsync(key));
        }

        [Fact]
        public Task Collection_Remove_Single_Document_Throws_When_Null()
        {
            var collection = GetCollection();

            return Assert.ThrowsAsync<ArgumentNullException>(() => collection.RemoveAsync(null as Document<Member>));
        }

        [Fact]
        public Task Collection_Remove_Multiple_Key_Throws_When_Invalid()
        {
            var collection = GetCollection();

            return Assert.ThrowsAsync<ArgumentNullException>(() => collection.RemoveAsync(null as IEnumerable<string>));
        }

        [Fact]
        public Task Collection_Remove_Multiple_Document_Throws_When_Null()
        {
            var collection = GetCollection();

            return Assert.ThrowsAsync<ArgumentNullException>(() => collection.RemoveAsync(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public async Task Collection_Remove_Single_Document_Raises_DocumentRemoved()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);
            var e = await Assert.RaisesAsync<DocumentRemovedEventArgs<Member>>(
                h => collection.DocumentRemoved += h,
                h => collection.DocumentRemoved -= h,
                () => collection.RemoveAsync(document));
            Assert.True(collection.IsEmpty);
            Assert.Equal(collection, e.Sender);
        }

        [Fact]
        public async Task Collection_Remove_Single_Key_Raises_DocumentRemoved()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            await collection.AddAsync(document);
            var e = await Assert.RaisesAsync<DocumentRemovedEventArgs<Member>>(
                h => collection.DocumentRemoved += h,
                h => collection.DocumentRemoved -= h,
                () => collection.RemoveAsync(document.Key));
            Assert.True(collection.IsEmpty);
            Assert.Equal(collection, e.Sender);
        }

        [Fact]
        public async Task Collection_Remove_Multiple_Key_Removes_Documents()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                document
            };

            await collection.AddAsync(documents);
            await collection.RemoveAsync(documents.Select(d => d.Key));

            Assert.True(collection.IsEmpty);
        }

        [Fact]
        public async Task Collection_Remove_Multiple_Document_Removes_Documents()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                document
            };

            await collection.AddAsync(documents);
            await collection.RemoveAsync(documents);
            Assert.True(collection.IsEmpty);
        }

        [Fact]
        public async Task Collection_Read_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ReadAsync(key));

            key = null;
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ReadAsync(key));

            key = " ";
            await Assert.ThrowsAsync<ArgumentException>(() => collection.ReadAsync(key));

            await Assert.ThrowsAsync<ArgumentNullException>(() => collection.ReadAsync(null as IEnumerable<string>));
        }

        [Fact]
        public async Task Collection_Read_Single_Returns_Document()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                document
            };

            await collection.AddAsync(documents);
            var read = await collection.ReadAsync(document.Key);

            Assert.Equal(document, read);
        }

        [Fact]
        public async Task Collection_Read_Multiple_Returns_Document()
        {
            var collection = GetCollection();

            var document1 = (Document<Member>)new Member();
            var document2 = (Document<Member>)new Member();
            var document3 = (Document<Member>)new Member();
            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                document1,
                document2,
                document3,
            };

            await collection.AddAsync(documents);
            var read = await collection.ReadAsync(new string[] { document1.Key, document2.Key });

            Assert.Contains(document1, read);
            Assert.Contains(document2, read);
            Assert.DoesNotContain(document3, read);
        }

        [Fact]
        public async Task Collection_Update_Fails_When_Null()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            await collection.AddAsync(document1);

            await Assert.ThrowsAsync<ArgumentNullException>(() => collection.UpdateAsync(null as Document<Member>));
        }

        [Fact(Skip = "etag check is disabled until transactions are implemented")]
        public async Task Collection_Update_Fails_When_ETags_Are_Mismatched()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            await collection.AddAsync(document1);

            member.Value = "x";
            var document2 = (Document<Member>)member;

            await Assert.ThrowsAsync<ETagMismatchException>(() => collection.UpdateAsync(document2));
        }

        [Fact]
        public async Task Collection_Update_Raises_DocumentUpdated()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            await collection.AddAsync(document1);

            var document2 = (Document<Member>)member;
            member.Value = "x";

            var e = await Assert.RaisesAsync<DocumentUpdatedEventArgs<Member>>(
                h => collection.DocumentUpdated += h,
                h => collection.DocumentUpdated -= h,
                () => collection.UpdateAsync(document2));
            Assert.Equal(collection, e.Sender);
        }

        [Fact]
        public async Task Collection_Update_Without_EventHandler()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            await collection.AddAsync(document1);

            var document2 = (Document<Member>)member;
            member.Value = "x";

            await collection.UpdateAsync(document2);
            var document3 = await collection.ReadAsync(document1.Key);

            Assert.Equal(document1.Key, document3.Key);
        }

        [Fact]
        public Task Collection_Update_Multiple_Throws_When_Null()
        {

            var collection = GetCollection();

            return Assert.ThrowsAsync<ArgumentNullException>(() =>
                collection.UpdateAsync(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public async Task Collection_Update_Multiple()
        {

            var collection = GetCollection();

            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
            };

            await collection.AddAsync(documents);

            var document1 = await collection.ReadAsync(documents[0].Key);
            var document2 = await collection.ReadAsync(documents[1].Key);

            ((Member)document1).Value = "x";
            ((Member)document2).Value = "y";

            await collection.UpdateAsync(new Document<Member>[] { document1, document2 });

            var document3 = await collection.ReadAsync(documents[0].Key);
            var document4 = await collection.ReadAsync(documents[1].Key);

            Assert.Equal(document1, document3);
            Assert.Equal(document2, document4);

            Assert.Equal("x", ((Member)document3).Value);
            Assert.Equal("y", ((Member)document4).Value);
        }
#pragma warning restore IDE1006 // Naming Styles
    }
}
