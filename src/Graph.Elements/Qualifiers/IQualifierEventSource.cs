using System;

namespace Graphs.Qualifiers
{
    public interface IQualifierEventSource
    {
        event EventHandler<QualifiedEventArgs> Qualified;
        event EventHandler<DisqualifiedEventArgs> Disqualified;
    }
}
