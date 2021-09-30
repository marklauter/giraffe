using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Documents
{
    [JsonObject]
    public sealed class Document<TMember>
        : ICloneable
        , IEquatable<Document<TMember>>
        , IEqualityComparer<Document<TMember>>
        where TMember : class
    {

        [JsonProperty]
        public int ETag { get; }


        [JsonProperty]
        public string Key { get; }


        [JsonProperty]
        public TMember Member { get; }

        private Document(Document<TMember> other)
            : this(other.Member, other.Key, other.ETag)
        {
        }

        [JsonConstructor]
        private Document(TMember member, string key, int etag)
        {
            this.Member = member;
            this.Key = key;
            this.ETag = etag;
        }


        public object Clone()
        {
            return new Document<TMember>(this);
        }


        public bool Equals(Document<TMember> other)
        {
            return other != null
                && other.Key.CompareTo(this.Key) == 0;
        }


        public bool Equals(Document<TMember> x, Document<TMember> y)
        {
            return x != null && x.Equals(y);
        }


        public override bool Equals(object obj)
        {
            return obj is Document<TMember> document
                && this.Equals(document);
        }


        public override int GetHashCode()
        {
            return HashCode.Combine(this.Key);
        }


        public int GetHashCode(Document<TMember> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }


        public static explicit operator TMember(Document<TMember> document)
        {
            return document.Member;
        }


        public static explicit operator Document<TMember>(TMember member)
        {
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            var key = KeyBuilder<TMember>.GetKey(member);

            return new Document<TMember>(
                member,
                key,
                HashCode.Combine(member, key));
        }
    }
}
