using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Documents.Tests
{
    public sealed class Member : IEquatable<Member>
    {
        [Key]
        public Guid Id { get; } = Guid.NewGuid();

        public string Value { get; set; } = String.Empty;

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Member);
        }

        public bool Equals(Member other)
        {
            return other != null &&
                   this.Id.Equals(other.Id) &&
                   this.Value == other.Value;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Id, this.Value);
        }

        public static bool operator ==(Member left, Member right)
        {
            return EqualityComparer<Member>.Default.Equals(left, right);
        }

        public static bool operator !=(Member left, Member right)
        {
            return !(left == right);
        }
    }
}
