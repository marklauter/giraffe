using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Graph.Elements
{
    public interface IElement
        : ICloneable
    {
        [Key]
        Guid Id { get; }

        /// <summary>
        /// Checks the map of attribute name-value pairs for a matching name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True if the element contains an attribute matching the name parameter.</returns>
        /// <remarks>
        /// <seealso cref="Attribute(String)"/>
        /// <seealso cref="Qualify(String, String)"/>
        /// <seealso cref="Qualify(IEnumerable{KeyValuePair{String, String}})"/>
        /// HasA
        /// </remarks>
        bool HasAttribute(string name);

        /// <summary>
        /// Sets adds an attribute, in the form of a name-value pair, to the element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns><see cref="IElement"/> for fluent access.</returns>
        /// <remarks>
        /// <seealso cref="Qualify(IEnumerable{KeyValuePair{String, String}})"/>
        /// <seealso cref="Attribute(String)"/>
        /// <seealso cref="HasAttribute(String)"/>
        /// </remarks>
        IElement Qualify(string name, string value);

        /// <summary>
        /// Apppends a set of attributes to the element.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns><see cref="IElement"/> for fluent access.</returns>
        /// <remarks>
        /// <seealso cref="Qualify(String, String)"/>
        /// <seealso cref="Attribute(String)"/>
        /// <seealso cref="HasAttribute(String)"/>
        /// </remarks>
        IElement Qualify(IEnumerable<KeyValuePair<string, string>> attributes);

        /// <summary>
        /// Gets the value of an attribute.
        /// </summary>
        /// <param name="name">Name of the attribute.</param>
        /// <returns>Attribute value.</returns>
        /// <remarks>
        /// <see cref="Qualify(String, String)"/>
        /// <seealso cref="Qualify(IEnumerable{KeyValuePair{String, String}})"/>
        /// <seealso cref="HasAttribute(String)"/>
        /// </remarks>
        bool TryGetAttribute(string name, out string value);
    }
}
