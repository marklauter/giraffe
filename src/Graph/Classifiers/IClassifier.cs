using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Graph.Classifiers
{
    public interface IClassifier<T>
        : ICloneable
    {
        /// <summary>
        /// Raised when one or more items are classified.
        /// </summary>
        /// <remarks><see cref="ItemsClassifiedEventArgs{T}"/></remarks>
        event EventHandler<ItemsClassifiedEventArgs<T>> ItemsClassified;

        /// <summary>
        /// Raised when one or more items are declassified.
        /// </summary>
        /// <remarks><see cref="ItemsDeclassifiedEventArgs{T}"/></remarks>
        event EventHandler<ItemsDeclassifiedEventArgs<T>> ItemsDeclassified;

        /// <summary>
        /// Adds an item to a class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <param name="item">The item to classify.</param>
        /// <returns><see cref="IClassifier{T}"/></returns>
        IClassifier<T> Classify(string label, T item);

        /// <summary>
        /// Adds a collection of items to a class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <param name="items">The items to classify.</param>
        /// <returns><see cref="IClassifier{T}"/></returns>
        IClassifier<T> Classify(string label, IEnumerable<T> items);

        /// <summary>
        /// Removes an item from the class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <param name="item">The item to declassify.</param>
        /// <returns><see cref="IClassifier{T}"/></returns>
        IClassifier<T> Declassify(string label, T item);

        /// <summary>
        /// Removes a collectio of items from the class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <param name="items">The items to declassify.</param>
        /// <returns><see cref="IClassifier{T}"/></returns>
        IClassifier<T> Declassify(string label, IEnumerable<T> items);

        /// <summary>
        /// Returns true if the class exists.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <returns><see cref="Boolean"/></returns>
        bool Exists(string label);

        /// <summary>
        /// Returns all the members of a class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <returns><see cref="ImmutableHashSet{T}"/></returns>
        ImmutableHashSet<T> Members(string label);

        /// <summary>
        /// Returns true if the item belongs to the class.
        /// </summary>
        /// <param name="label">Name of the class.</param>
        /// <param name="item">The item to check for membership in the class.</param>
        /// <returns><see cref="Boolean"/></returns>
        bool Is(string label, T item);
    }
}
