using System;
using System.ComponentModel.DataAnnotations;

namespace Documents.Tests
{
    public sealed class Member
    {
        [Key]
        public Guid Id { get; } = Guid.NewGuid();
    }
}
