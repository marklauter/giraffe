using System;

namespace Graphs.Elements.Classifiers
{
    public interface IClassifierEventSource<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        /// <summary>
        /// Raised when item is assigned to a classification.
        /// <see cref="ClassifiedEventArgs"/>
        /// </summary>
        /// <remarks>
        /// <seealso cref="Declassified"/>
        /// </remarks>
        event EventHandler<ClassifiedEventArgs<TId>> Classified;

        /// <summary>
        /// Raised when item is removed from a classification.
        /// <see cref="DeclassifiedEventArgs"/>
        /// </summary>
        /// <remarks>
        /// <seealso cref="Classified"/>
        /// </remarks>
        event EventHandler<DeclassifiedEventArgs<TId>> Declassified;
    }
}
