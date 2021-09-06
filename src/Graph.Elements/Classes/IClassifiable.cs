using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    public interface IClassifiable
    {
        event EventHandler<ClassifiedEventArgs> Classified;
        event EventHandler<DeclassifiedEventArgs> Declassified;

        IClassifiable Classify(string label);
        IClassifiable Declassify(string label);
        bool Is(string label);
        bool Is(IEnumerable<string> labels);
    }
}
