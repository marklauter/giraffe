using System;

namespace Graphs.Elements.Qualifiers
{
    public interface IQualifierEventSource
    {
        event EventHandler<QualifiedEventArgs> Qualified;
        event EventHandler<DisqualifiedEventArgs> Disqualified;
    }
}
