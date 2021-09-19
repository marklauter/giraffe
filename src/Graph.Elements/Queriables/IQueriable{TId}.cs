using Graphs.Elements.Identifiers;
using System;
using System.Collections.Generic;

namespace Graphs.Elements.Queriables
{
    public interface IQueriable<TId>
        : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        /// <summary>
        /// Returns true if the instance contains the named attribute (quantity or quality).
        /// </summary>
        /// <param name="name">Name of the attribute (quantity or quality).</param>
        /// <returns><see cref="Boolean"/></returns>
        bool HasProperty(string name);

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

        /// <summary>
        /// Returns the value of the named quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality value to return.</param>
        /// <returns><see cref="String"/></returns>
        /// <remarks>
        /// <seealso cref="HasQuality(String)"/>
        /// </remarks>
        bool TryGetProperty(string name, out object value);
    }
}
