using System;

namespace JenkinsNET.Exceptions
{
    /// <summary>
    /// Base-Class for all Jenkins.Net exceptions.
    /// </summary>
    public class JenkinsNetException : ApplicationException
    {
        internal JenkinsNetException(string message) : base(message) {}
        internal JenkinsNetException(string message, Exception innerException) : base(message, innerException) {}
    }
}
