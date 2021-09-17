using System;
using System.Collections.Generic;

namespace Graphs.Classifiers
{
    public interface IClassifier
        : IClassifierEventSource
        , ICloneable
    {
        /// <summary>
        /// Classifies this instance.
        /// </summary>
        /// <param name="label"></param>
        /// <returns><see cref="IClassifier"/></returns>
        /// <remarks>
        /// Raises event <see cref="Classified"/>
        /// <seealso cref="Declassify"/>
        /// </remarks>
        IClassifier Classify(string label);

        /// <summary>
        /// Declassifies this instance.
        /// </summary>
        /// <param name="label"></param>
        /// <returns><see cref="IClassifier"/></returns>
        /// <remarks>
        /// Raises event <see cref="Delassified"/>
        /// <see cref="DeclassifiedEventArgs"/>
        /// <seealso cref="Classify"/>
        /// </remarks>
        IClassifier Declassify(string label);

        /// <summary>
        /// Returns true if the instance is a member of class referenced by label.
        /// </summary>
        /// <param name="label">A class label.</param>
        /// <returns><see cref="Boolean"/></returns>
        /// <remarks>
        /// <seealso cref="Is(IEnumerable{String})"/>
        /// </remarks>
        bool Is(string label);

        /// <summary>
        /// Returns true if the instance is a member of every class referenced by the collection of labels.
        /// IE: The set of labeled classes is a superset of the collection of class labels.
        /// </summary>
        /// <param name="labels">A set of class labels.</param>
        /// <returns><see cref="Boolean"/></returns>
        /// <remarks>
        /// <seealso cref="Is(String)"/>
        /// </remarks>
        bool Is(IEnumerable<string> labels);
    }
}
