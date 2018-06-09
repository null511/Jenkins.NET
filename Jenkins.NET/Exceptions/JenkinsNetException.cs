using System;

namespace JenkinsNET.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Base class for all Jenkins.Net exceptions.
    /// </summary>
    public class JenkinsNetException : Exception
    {
        internal JenkinsNetException(string message) : base(message) {}
        internal JenkinsNetException(string message, Exception innerException) : base(message, innerException) {}
    }
}
