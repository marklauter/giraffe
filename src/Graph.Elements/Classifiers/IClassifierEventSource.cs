using System;

namespace Graphs.Classifiers
{
    public interface IClassifierEventSource
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
    }
}
