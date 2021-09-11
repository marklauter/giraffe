using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

namespace Documents
{
    [JsonObject]
    public sealed class Document<TMember>
        : ICloneable
        , IEquatable<Document<TMember>>
        , IEqualityComparer<Document<TMember>>
        where TMember : class
    {
        internal static PropertyInfo[] KeyProperties { get; } =
            typeof(TMember).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.GetCustomAttribute<KeyAttribute>() != null)
                .ToArray();

        private Document([Pure] Document<TMember> other)
            : this(other.Member, other.Key, other.ETag)
        {
        }

        [JsonConstructor]
        private Document([Pure] TMember member, string key, int etag)
        {
            this.Member = member;
            this.Key = key;
            this.ETag = etag;
        }

        [Pure]
        [JsonProperty]
        public int ETag { get; }

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
            if (member is null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            var key = GetKey(member);

            return new Document<TMember>(
                member,
                key,
                HashCode.Combine(member, key));
        }

        [Pure]
        private static string BuildKey([Pure] TMember member)
        {
            var keys = new string[KeyProperties.Length];
            for (var i = 0; i < keys.Length; ++i)
            {
                keys[i] = KeyProperties[i].GetValue(member).ToString();
            }

            return String.Join('.', keys);
        }

        [Pure]
        private static string GetKey([Pure] TMember member)
        {
            return KeyProperties.Length > 0
                ? BuildKey(member)
                : throw new ArgumentException($"Argument {nameof(member)} of type {typeof(TMember).FullName} has no properties targeted with [{typeof(KeyAttribute).FullName}].");
        }
    }
}
