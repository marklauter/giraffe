using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("{Offset}, {Degree}, Deleted {IsDeleted}, => {FirstEdgeOffset}")]
    public struct NodeRecord
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

        public long Offset { get; }

        // offset 0
        public long FirstEdgeOffset { get; }

        // offset sizeof(long)
        public bool IsDeleted { get; }

        // offset sizeof(long) + sizeof(bool)
        public int Degree { get; }
    }
}

