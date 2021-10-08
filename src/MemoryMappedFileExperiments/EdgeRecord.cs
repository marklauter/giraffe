using System.Diagnostics;

namespace MemoryMappedFileExperiments
{
    [DebuggerDisplay("({Source}, {Target}), IsActive {IsActive}, => {NextRecord}")]
    public struct EdgeRecord
    {
        public EdgeRecord(long nextRecord, bool isActive, long source, long target)
        {
            this.NextRecord = nextRecord;
            this.IsActive = isActive;
            this.Source = source;
            this.Target = target;
        }

        // points to offset of next neighbor record
        public long NextRecord { get; }

        // set to false to remove the edge from traversal, but edge must still exist for linked list
        public bool IsActive { get; }

        public long Source { get; }

        public long Target { get; }
    }
}

