using System;

namespace Graphs.Elements
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
