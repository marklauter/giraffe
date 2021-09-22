using System;

namespace Graphs.Elements
{
    public sealed class GuidGenerator
        : IIdGenerator<Guid>
    {
        public Guid NewId()
        {
            return Guid.NewGuid();
        }
    }
}
