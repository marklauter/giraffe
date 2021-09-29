using Graphs.Events;
using System;

namespace Graphs.IO.Output
{
    public sealed class GraphEventConsumer<TId>
        : IDisposable
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        private readonly IGraphEventSource<TId> eventSource;
        private bool disposedValue;

        public GraphEventConsumer(IGraphEventSource<TId> eventSource)
        {
            this.eventSource = eventSource ?? throw new ArgumentNullException(nameof(eventSource));

            this.eventSource.Classified += this.EventSource_Classified;
            this.eventSource.Declassified += this.EventSource_Declassified;

            this.eventSource.Connected += this.EventSource_Connected;
            this.eventSource.Disconnected += this.EventSource_Disconnected;

            this.eventSource.Qualified += this.EventSource_Qualified;
            this.eventSource.Disqualified += this.EventSource_Disqualified;

            this.eventSource.NodeAdded += this.EventSource_NodeAdded;
            this.eventSource.NodeRemoved += this.EventSource_NodeRemoved;
        }

        private void EventSource_NodeRemoved(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_NodeAdded(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Disqualified(object sender, Elements.Qualifiers.DisqualifiedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Qualified(object sender, Elements.Qualifiers.QualifiedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Disconnected(object sender, Elements.Nodes.DisconnectedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Connected(object sender, Elements.Nodes.ConnectedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Declassified(object sender, Elements.Classifiers.DeclassifiedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void EventSource_Classified(object sender, Elements.Classifiers.ClassifiedEventArgs<TId> e)
        {
            throw new NotImplementedException();
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    this.eventSource.Classified -= this.EventSource_Classified;
                    this.eventSource.Declassified -= this.EventSource_Declassified;

                    this.eventSource.Connected -= this.EventSource_Connected;
                    this.eventSource.Disconnected -= this.EventSource_Disconnected;

                    this.eventSource.Qualified -= this.EventSource_Qualified;
                    this.eventSource.Disqualified -= this.EventSource_Disqualified;

                    this.eventSource.NodeAdded -= this.EventSource_NodeAdded;
                    this.eventSource.NodeRemoved -= this.EventSource_NodeRemoved;
                }

                this.disposedValue = true;
            }
        }

        public void Dispose()
        {
            this.Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}


