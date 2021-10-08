using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("{Degree}, => {FirstEdgeOffset}")]
    public struct NodeRecord
    {
        internal NodeRecord(int degree, long firstEdgeOffset)
        {
            this.Degree = degree;
            this.FirstEdgeOffset = firstEdgeOffset;
        }

        public int Degree { get; }
        public long FirstEdgeOffset { get; }
    }
}

