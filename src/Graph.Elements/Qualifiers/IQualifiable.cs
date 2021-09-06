using System;

namespace Graph.Elements
{
    public interface IQualifiable
    {
        event EventHandler<QualificationChangedEventArgs> QualificationChanged;
        event EventHandler<QualificationRejectedEventArgs> QualificationRejected;

        /// <summary>
        /// Removes a quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality to remove.</param>
        /// <returns><see cref="IQualifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Qualify(String, String)"/>
        /// </remarks>
        IQualifiable Reject(string name);

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
        /// <returns><see cref="IQualifiable"/></returns>
        /// <remarks>
        /// <seealso cref="Reject(String)"/>
        /// </remarks>
        IQualifiable Qualify(string name, string value);

        /// <summary>
        /// Returns the value of the named quality attribute.
        /// </summary>
        /// <param name="name">Name of the quality value to return.</param>
        /// <returns><see cref="String"/></returns>
        /// <remarks>
        /// <seealso cref="HasQuality(String)"/>
        /// </remarks>
        string Quality(string name);
    }
}
