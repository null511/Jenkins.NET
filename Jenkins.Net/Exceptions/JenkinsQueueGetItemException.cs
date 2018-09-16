using System;

namespace JenkinsNET.Exceptions
{
    public class JenkinsQueueGetItemException : JenkinsNetException
    {
        internal JenkinsQueueGetItemException(string message, Exception innerException) : base(message, innerException) {}
    }
}
