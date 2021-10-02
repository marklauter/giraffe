using System;

namespace Graphs.Identifiers
{
    public sealed class DefaultIdGenerator
        : IIdGenerator<Guid>
    {
        public Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}
