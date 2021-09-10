using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace Documents
{

    [JsonObject]
    public sealed class Document<TMember>
        : ICloneable
        , IEquatable<Document<TMember>>
        , IEqualityComparer<Document<TMember>>
        where TMember : class
    {
        private Document([Pure] Document<TMember> other)
            : this(other.Member, other.Key, Guid.NewGuid())
        {
        }

        [JsonConstructor]
        private Document([Pure] TMember member, string key, Guid etag)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException($"'{nameof(key)}' cannot be null or whitespace.", nameof(key));
            }

            this.Member = member ?? throw new ArgumentNullException(nameof(member));
            this.Key = key;
            this.ETag = etag;
        }

        [Pure]
        [JsonProperty]
        public Guid ETag { get; }

        [Pure]
        [JsonProperty]
        public string Key { get; }

        [Pure]
        [JsonProperty]
        internal TMember Member { get; }

        [Pure]
        public object Clone()
        {
            return new Document<TMember>(this);
        }

        [Pure]
        public bool Equals([Pure] Document<TMember> other)
        {
            return other != null
                && other.Key.CompareTo(this.Key) == 0;
        }

        [Pure]
        public bool Equals([Pure] Document<TMember> x, [Pure] Document<TMember> y)
        {
            return x != null && x.Equals(y);
        }

        [Pure]
        public override bool Equals([Pure] object obj)
        {
            return obj is Document<TMember> document
                && this.Equals(document);
        }

        [Pure]
        public override int GetHashCode()
        {
            return HashCode.Combine(this.Key);
        }

        [Pure]
        public int GetHashCode([DisallowNull, Pure] Document<TMember> obj)
        {
            return obj is null
                ? throw new ArgumentNullException(nameof(obj))
                : obj.GetHashCode();
        }

        [Pure]
        public static explicit operator TMember([DisallowNull, Pure] Document<TMember> document)
        {
            return document.Member;
        }

        [Pure]
        public static explicit operator Document<TMember>([DisallowNull, Pure] TMember member)
        {
            return new Document<TMember>(member, GetKey(member), Guid.NewGuid());
        }

        [Pure]
        private static string BuildKey([Pure] TMember member)
        {
            var keys = new string[DocumentKeys<TMember>.KeyProperties.Length];
            for (var i = 0; i < keys.Length; ++i)
            {
                keys[i] = DocumentKeys<TMember>.KeyProperties[i].GetValue(member).ToString();
            }

            return String.Join('.', keys);
        }

        [Pure]
        private static string GetKey([Pure] TMember member)
        {
            return DocumentKeys<TMember>.KeyProperties.Length > 0
                ? BuildKey(member)
                : throw new ArgumentException($"Argument {nameof(member)} of type {typeof(TMember).FullName} has no properties targeted with [{typeof(KeyAttribute).FullName}].");
        }
    }
}
