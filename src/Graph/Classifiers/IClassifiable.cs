using System;
using System.Collections.Generic;

namespace Graph.Classifiers
{
    public interface IClassifiable
        : ICloneable
    {
        /// <summary>
        /// Raised when item is assigned to a classification.
        /// <see cref="ClassifiedEventArgs"/>
        /// </summary>
        /// <remarks>
        /// <seealso cref="Declassified"/>
        /// </remarks>
        event EventHandler<ClassifiedEventArgs> Classified;

        /// <summary>
        /// Raised when item is removed from a classification.
        /// <see cref="DeclassifiedEventArgs"/>
        /// </summary>
        /// <remarks>
        /// <seealso cref="Classified"/>
        /// </remarks>
        event EventHandler<DeclassifiedEventArgs> Declassified;

        /// <summary>
        /// Classifies this instance.
        /// </summary>
        /// <param name="label"></param>
        /// <returns><see cref="IClassifiable"/></returns>
        /// <remarks>
        /// Raises event <see cref="Classified"/>
        /// <seealso cref="Declassify"/>
        /// </remarks>
        IClassifiable Classify(string label);

        /// <summary>
        /// Declassifies this instance.
        /// </summary>
        /// <param name="label"></param>
        /// <returns><see cref="IClassifiable"/></returns>
        /// <remarks>
        /// Raises event <see cref="Delassified"/>
        /// <see cref="DeclassifiedEventArgs"/>
        /// <seealso cref="Classify"/>
        /// </remarks>
        IClassifiable Declassify(string label);

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
        /// Returns true if the instance is a member of any class referenced by the collection of labels.
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
