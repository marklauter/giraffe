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
        IQualifier Qualify(string name, bool value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, sbyte value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, byte value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, short value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, ushort value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, int value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, uint value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, long value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, ulong value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, float value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, double value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, decimal value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, DateTime value);

        /// <summary>
        /// Adds a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality.</param>
        /// <param name="value">Value of the quality.</param>
        /// <returns><see cref="IQualifier"/></returns>
        /// <remarks>
        /// <seealso cref="Disqualify(String)"/>
        /// </remarks>
        IQualifier Qualify(string name, string value);

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
