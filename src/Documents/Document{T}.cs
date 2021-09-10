﻿using Newtonsoft.Json;
using System;

namespace Documents
{
    [JsonObject]
    public sealed class Document<T>
        : ICloneable
        where T : class
    {
        private Document(Document<T> other)
            : this(other.Member, other.Key, Guid.NewGuid())
        {
        }

        [JsonConstructor]
        private Document(T member, string key, Guid etag)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            this.Member = member ?? throw new ArgumentNullException(nameof(member));
            this.Key = key;
            this.ETag = etag;
        }

        [JsonProperty]
        public Guid ETag { get; }

        [JsonProperty]
        public string Key { get; }

        [JsonProperty]
        internal T Member { get; }

        public object Clone()
        {
            return new Document<T>(this);
        }

        private static string GetKey(T member)
        {
            return DocumentKeys<T>.KeyProperties.Length > 0
                ? BuildKey(member)
                : member.GetHashCode().ToString();
        }

        private static string BuildKey(T member)
        {
            var keys = new string[DocumentKeys<T>.KeyProperties.Length];
            for (var i = 0; i < keys.Length; ++i)
            {
                keys[i] = DocumentKeys<T>.KeyProperties[i].GetValue(member).ToString();
            }

            return String.Join('.', keys);
        }

        public override bool Equals(object obj)
        {
            return obj is Document<T> document &&
                   this.Key == document.Key;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Key);
        }

        public static explicit operator T(Document<T> document)
        {
            return document.Member;
        }

        public static explicit operator Document<T>(T member)
        {
            return new Document<T>(member, GetKey(member), Guid.NewGuid());
        }
    }
}
