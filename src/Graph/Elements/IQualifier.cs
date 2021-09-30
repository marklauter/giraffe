using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface IQualifier
    {
        IEnumerable<KeyValuePair<string, object>> Attributes { get; }

        void Disqualify(string name);

        bool HasAttribute(string name);

        void Qualify(string name, object value);

        bool TryGetValue(string name, out object value);
    }
}
