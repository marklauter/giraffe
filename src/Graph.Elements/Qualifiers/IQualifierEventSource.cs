using System;

namespace Graph.Qualifiers
{
    public interface IQualifierEventSource
    {
        event EventHandler<QualifiedEventArgs> Qualified;
        event EventHandler<DisqualifiedEventArgs> Disqualified;
    }
}
