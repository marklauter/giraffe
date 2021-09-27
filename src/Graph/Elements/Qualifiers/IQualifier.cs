using System;

namespace Graphs.Elements.Qualifiers
{
    public interface IQualifier
        : IQualifierEventSource
        , ICloneable
    {
        /// <summary>
        /// Removes a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality to remove.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Qualify(String, String)"/>
        /// </remarks>
        IQualifier Disqualify(string name);

        /// <summary>
        /// Returns true if the instance contains the named quality.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <returns><see cref="Boolean"/></returns>
        /// <remarks>
        /// <seealso cref="Quality(String)"/>
        /// </remarks>
        bool HasQuality(string name);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, object value);

        /// <summary>
        /// Returns the value of the named quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality value to return.</param>
        /// <returns><see cref="String"/></returns>
        /// <remarks>
        /// <seealso cref="HasQuality(String)"/>
        /// </remarks>
        bool TryGetValue(string name, out object value);
    }
}
