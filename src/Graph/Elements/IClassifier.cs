using System.Collections.Generic;

namespace Graphs.Elements
{
    public interface IClassifier
    {
        IEnumerable<string> Labels { get; }

        void Classify(string label);

        void Declassify(string label);

        bool Is(string label);

        bool Is(IEnumerable<string> labels);
    }
}
