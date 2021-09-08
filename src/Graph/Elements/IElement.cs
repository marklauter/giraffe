using Graph.Classifiers;
using Graph.Identifiers;
using Graph.Qualifiers;
using Graph.Quantifiers;
using System;
using System.Collections.Generic;

namespace Graph.Elements
{
    public interface IElement<TId>
        : IIdentifiable<TId>
        where TId : struct, IComparable, IComparable<TId>, IEquatable<TId>, IFormattable
    {
        #region classification
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
        IElement<TId> Classify(string label);

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
        IElement<TId> Declassify(string label);

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
        #endregion

        #region qualification
        event EventHandler<QualifiedEventArgs> Qualified;
        event EventHandler<DisqualifiedEventArgs> Disqualified;

        /// <summary>
        /// Removes a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality to remove.</param>
        /// <returns><see cref="IQualifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Qualify(String, String)"/>
        /// </remarks>
        IElement<TId> Disqualify(string name);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IElement<TId> Qualify(string name, string value);

        /// <summary>
        /// Returns the value of the named quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality value to return.</param>
        /// <returns><see cref="String"/></returns>
        /// <remarks>
        /// <seealso cref="HasQuality(String)"/>
        /// </remarks>
        string Quality(string name);
        #endregion

        #region quatification
        event EventHandler<QuantifiedEventArgs> Quantified;
        event EventHandler<QuantityRemovedEventArgs> QuantityRemoved;

        /// <summary>
        /// Removes a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity to remove.</param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Quantify"/>
        /// </remarks>
        IElement<TId> RemoveQuantity(string name);

        /// <summary>
        /// Adds a quantity attribute.
        /// </summary>
        /// <param name="name">Name of the quantity.</param>
        /// <param name="quantity"><see cref="IQuantity"/></param>
        /// <returns><see cref="IQuantifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Ignore"/>
        /// </remarks>
        IElement<TId> Quantify(Quantity quantity);

        /// <summary>
        /// Returns the named quanity.
        /// </summary>
        /// <param name="name">Name of the quantity to return.</param>
        /// <returns><see cref="IQuantity"/></returns>
        /// <remarks>
        /// <seealso cref="Value{T}"/>
        /// </remarks>
        Quantity Quantity(string name);
        #endregion

        /// <summary>
        /// Returns true if the instance contains the named attribute (quantity or quality).
        /// </summary>
        /// <param name="name">Name of the attribute (quantity or quality).</param>
        /// <returns><see cref="Boolean"/></returns>
        bool HasAttribute(string name);
    }
}
