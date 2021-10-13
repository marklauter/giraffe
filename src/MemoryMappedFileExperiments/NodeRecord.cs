using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("{Offset}, {Degree}, Deleted {IsDeleted}, => {FirstEdgeOffset}")]
    public readonly struct MemoryMappedNode
    {
        internal MemoryMappedNode(
            long firstEdgeOffset,
            bool isDeleted,
            int degree)
        {
            this.Degree = degree;
            this.IsDeleted = isDeleted;
            this.FirstEdgeOffset = firstEdgeOffset;
        }

        // offset 0
        public readonly long FirstEdgeOffset;

        // offset sizeof(long)
        public readonly bool IsDeleted;

        // offset sizeof(long) + sizeof(bool)
        public readonly int Degree;
    }

    [DebuggerDisplay("{Offset}, {Degree}, Deleted {IsDeleted}, => {FirstEdgeOffset}")]
    public readonly struct NodeRecord
    {
        internal NodeRecord(
            long offset,
            long firstEdgeOffset,
            bool isDeleted,
            int degree)
        {
            this.Offset = offset;
            this.Degree = degree;
            this.IsDeleted = isDeleted;
            this.FirstEdgeOffset = firstEdgeOffset;
        }

        public readonly long Offset;

        // offset 0
        public readonly long FirstEdgeOffset;

        // offset sizeof(long)
        public readonly bool IsDeleted;

        // offset sizeof(long) + sizeof(bool)
        public readonly int Degree;
    }
}

