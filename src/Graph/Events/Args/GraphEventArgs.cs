using System;

namespace Graphs.Events
{
    public abstract class GraphEventArgs
        : EventArgs
    {
        protected GraphEventArgs()
        {
            this.Timestamp = DateTime.UtcNow;
            this.EventId = Guid.NewGuid();
        }

        public DateTime Timestamp { get; }

        public Guid EventId { get; }
    }
}
