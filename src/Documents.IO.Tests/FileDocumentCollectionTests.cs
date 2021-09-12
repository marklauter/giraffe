using Documents.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Xunit;

namespace Documents.IO.Tests
{
    public sealed class FileTestsFixture
        : IDisposable
    {
        public static string Path { get; } = Guid.NewGuid().ToString();

        public FileTestsFixture()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
        }

        public static string MakePath()
        {
            var path = System.IO.Path.Combine(Path, Guid.NewGuid().ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }

        public void Dispose()
        {
            if (Directory.Exists(Path))
            {
                Directory.Delete(Path, true);
            }
        }
    }

    public sealed class FileDocumentCollectionTests
        : IClassFixture<FileTestsFixture>
    {
        private static FileDocumentCollection<Member> GetCollection()
        {
            return new FileDocumentCollection<Member>(FileTestsFixture.MakePath(), TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void Collection_Empty_Contains_Zero_Items()
        {
            var collection = GetCollection();
            Assert.Equal(0, collection.Count);
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Add_Single_Throws_ArgumentNullException()
        {
            var collection = GetCollection();
            Assert.Throws<ArgumentNullException>(() => collection.Add(null as Document<Member>));
        }

        [Fact]
        public void Collection_Add_Multiple_Throws_ArgumentNullException()
        {
            var collection = GetCollection();
            Assert.Throws<ArgumentNullException>(() => collection.Add(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public void Collection_Add_Changes_Count()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);

            Assert.Equal(1, collection.Count);
            Assert.Contains(document, collection);
        }

        [Fact]
        public void Collection_Add_Multiple_Changes_Count()
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

            collection.Add(documents);
            Assert.Equal(documents.Length, collection.Count);
            Assert.True(collection.Contains(document));
        }

        [Fact]
        public void Collection_Add_Raises_DocumentAdded()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            var e = Assert.Raises<DocumentAddedEventArgs<Member>>(
                h => collection.DocumentAdded += h,
                h => collection.DocumentAdded -= h,
                () => collection.Add(document));
            Assert.Equal(collection, e.Sender);
            Assert.Equal(document, e.Arguments.Document);
        }

        [Fact]
        public void Collection_Contains_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Contains(key));

            key = null;
            Assert.Throws<ArgumentException>(() => collection.Contains(key));

            key = " ";
            Assert.Throws<ArgumentException>(() => collection.Contains(key));

            Assert.Throws<ArgumentNullException>(() => collection.Contains(null as Document<Member>));
        }

        [Fact]
        public void Collection_Contains_Document_Returns_True()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);

            Assert.True(collection.Contains(document));
        }

        [Fact]
        public void Collection_Contains_Document_Returns_False()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);

            Assert.False(collection.Contains((Document<Member>)new Member()));
        }

        [Fact]
        public void Collection_Contains_Key_Returns_True()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);

            Assert.True(collection.Contains(document.Key));
        }

        [Fact]
        public void Collection_Contains_Key_Returns_False()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);

            Assert.False(collection.Contains(((Document<Member>)new Member()).Key));
        }

        [Fact]
        public void Collection_Clear_Empties_Collection_And_Raises_Cleared()
        {
            var collection = GetCollection();

            collection
                .Add((Document<Member>)new Member())
                .Add((Document<Member>)new Member())
                .Add((Document<Member>)new Member());
            Assert.Equal(3, collection.Count);
            var e = Assert.Raises<EventArgs>(
                h => collection.Cleared += h,
                h => collection.Cleared -= h,
                () => collection.Clear());
            Assert.Equal(collection, e.Sender);
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Clear_Empties_Collection()
        {
            var collection = GetCollection();

            collection
                .Add((Document<Member>)new Member())
                .Add((Document<Member>)new Member())
                .Add((Document<Member>)new Member())
                .Clear();
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Remove_Single_Key_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Remove(key));

            key = null;
            Assert.Throws<ArgumentException>(() => collection.Remove(key));

            key = " ";
            Assert.Throws<ArgumentException>(() => collection.Remove(key));
        }

        [Fact]
        public void Collection_Remove_Single_Document_Throws_When_Null()
        {
            var collection = GetCollection();

            Assert.Throws<ArgumentNullException>(() => collection.Remove(null as Document<Member>));
        }

        [Fact]
        public void Collection_Remove_Multiple_Key_Throws_When_Invalid()
        {
            var collection = GetCollection();

            Assert.Throws<ArgumentNullException>(() => collection.Remove(null as IEnumerable<string>));
        }

        [Fact]
        public void Collection_Remove_Multiple_Document_Throws_When_Null()
        {
            var collection = GetCollection();

            Assert.Throws<ArgumentNullException>(() => collection.Remove(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public void Collection_Remove_Single_Document_Raises_DocumentRemoved()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);
            var e = Assert.Raises<DocumentRemovedEventArgs<Member>>(
                h => collection.DocumentRemoved += h,
                h => collection.DocumentRemoved -= h,
                () => collection.Remove(document));
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Remove_Single_Key_Raises_DocumentRemoved()
        {
            var collection = GetCollection();

            var document = (Document<Member>)new Member();
            collection.Add(document);
            var e = Assert.Raises<DocumentRemovedEventArgs<Member>>(
                h => collection.DocumentRemoved += h,
                h => collection.DocumentRemoved -= h,
                () => collection.Remove(document.Key));
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Remove_Multiple_Key_Removes_Documents()
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

            collection.Add(documents);

            collection.Remove(documents.Select(d => d.Key));
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Remove_Multiple_Document_Removes_Documents()
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

            collection.Add(documents);
            collection.Remove(documents);
            Assert.Empty(collection);
        }

        [Fact]
        public void Collection_Read_Throws_When_Invalid()
        {
            var collection = GetCollection();

            var key = String.Empty;
            Assert.Throws<ArgumentException>(() => collection.Read(key));

            key = null;
            Assert.Throws<ArgumentException>(() => collection.Read(key));

            key = " ";
            Assert.Throws<ArgumentException>(() => collection.Read(key));

            Assert.Throws<ArgumentNullException>(() => collection.Read(null as IEnumerable<string>));
        }

        [Fact]
        public void Collection_Read_Single_Returns_Document()
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

            var read = collection
                .Add(documents)
                .Read(document.Key);

            Assert.Equal(document, read);
        }

        [Fact]
        public void Collection_Read_Multiple_Returns_Document()
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

            var read = collection
                .Add(documents)
                .Read(new string[] { document1.Key, document2.Key });

            Assert.Contains(document1, read);
            Assert.Contains(document2, read);
            Assert.DoesNotContain(document3, read);
        }

        [Fact]
        public void Collection_Update_Fails_When_Null()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            collection.Add(document1);

            Assert.Throws<ArgumentNullException>(() => collection.Update(null as Document<Member>));
        }

        [Fact]
        public void Collection_Update_Fails_When_ETags_Are_Mismatched()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            collection.Add(document1);

            member.Value = "x";
            var document2 = (Document<Member>)member;

            Assert.Throws<ETagMismatchException>(() => collection.Update(document2));
        }

        [Fact]
        public void Collection_Update_Raises_DocumentUpdated()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            collection.Add(document1);

            var document2 = (Document<Member>)member;
            member.Value = "x";

            Assert.Raises<DocumentUpdatedEventArgs<Member>>(
                h => collection.DocumentUpdated += h,
                h => collection.DocumentUpdated -= h,
                () => collection.Update(document2));
        }

        [Fact]
        public void Collection_Update_Without_EventHandler()
        {
            var collection = GetCollection();

            var member = new Member();
            var document1 = (Document<Member>)member;
            collection.Add(document1);

            var document2 = (Document<Member>)member;
            member.Value = "x";

            var document3 = collection
                .Update(document2)
                .Read(document1.Key);

            Assert.Equal(document1.Key, document3.Key);
        }

        [Fact]
        public void Collection_Update_Multiple_Throws_When_Null()
        {

            var collection = GetCollection();

            Assert.Throws<ArgumentNullException>(() =>
                collection.Update(null as IEnumerable<Document<Member>>));
        }

        [Fact]
        public void Collection_Update_Multiple()
        {

            var collection = GetCollection();

            var documents = new Document<Member>[]
            {
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
                (Document<Member>)new Member(),
            };

            collection.Add(documents);

            var document1 = collection.Read(documents[0].Key);
            var document2 = collection.Read(documents[1].Key);

            document1.Member.Value = "x";
            document2.Member.Value = "y";

            collection.Update(new Document<Member>[] { document1, document2 });

            var document3 = collection.Read(documents[0].Key);
            var document4 = collection.Read(documents[1].Key);

            Assert.Equal(document1, document3);
            Assert.Equal(document2, document4);

            Assert.Equal("x", document3.Member.Value);
            Assert.Equal("y", document4.Member.Value);
        }
    }
}
