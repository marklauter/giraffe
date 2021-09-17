using System;
using System.Collections.Generic;

namespace Graphs.Classifiers
{
    /// <summary>
    /// A class is a labeled collection of element references 
    /// </summary>
    /// <typeparam name="TId">type of the ID</typeparam>
    public interface IClass<TId>
        : ICloneable
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

        string Label { get; }

        IClass<TId> Classify(TId id);
        IClass<TId> Classify(IEnumerable<TId> id);

        bool Contains(TId id);

        IClass<TId> Declassify(TId id);
        IClass<TId> Declassify(IEnumerable<TId> id);
    }
}
